using IceRpc.Slice;
using Microsoft.Extensions.Logging;
using SageKing.Core.Contracts;
using SageKing.Core.Extensions;
using SageKing.IceRPC.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Server.Services
{
    /// <summary>
    /// 客户端连接管理
    /// </summary>
    public class ClientConnectionInfoManagement : IClientConnectionInfoManagement<IConnectionContext, StreamPackage>
    {
        private readonly ILogger logger;
        private readonly IMediator _mediator;

        private static readonly object _lock = new object();
        private static readonly object _lockPush = new object();
        private readonly ConcurrentDictionary<string, ClientConnectionInfo<IConnectionContext>> _clientsDic;

        public ClientConnectionInfoManagement(IMediator mediator, ILoggerFactory loggerFactory)
        {
            _mediator = mediator;
            logger = loggerFactory.CreateLogger<ClientConnectionInfoManagement>();
            _clientsDic = new ConcurrentDictionary<string, ClientConnectionInfo<IConnectionContext>>();
        }

        public int AddClientConnectionInfo(string clientId, string connectionId, EndPoint remteAddress, int type, IConnectionContext conn, string iceProxyId)
        {
            lock (_lock)
            {
                if (_clientsDic.TryGetValue(connectionId, out var oldInfo))
                {
                    //清理旧对象
                    logger.LogWarning($"HeartBeat:开始断开旧对象:{connectionId}:{oldInfo.IceProxyId}:{oldInfo.RemoteAddress}");
                    var result1 = (oldInfo.Connection.Invoker as IProtocolConnection)?.ShutdownAsync().ConfigureAwait(false);
                    var result2 = (oldInfo.Connection.Invoker as IProtocolConnection)?.DisposeAsync().ConfigureAwait(false);
                    logger.LogWarning($"HeartBeat:开始清理旧对象:{connectionId}:{oldInfo.IceProxyId}:{oldInfo.RemoteAddress}");
                    //删除Session
                    _clientsDic.TryRemove(connectionId, out _);
                }

                var info = new ClientConnectionInfo<IConnectionContext>();

                info.ClientId = clientId;
                info.ConnectionId = connectionId;
                info.IceProxyId = iceProxyId;
                info.RemoteAddress = remteAddress;
                info.Connection = conn;

                info.ClientType = type.GetValue<ClientType>((int)ClientType.UNKNOW);
                info.LoginDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var isAdd = _clientsDic.TryAdd(connectionId, info);

                logger.LogDebug($"HeartBeat:AddClient:{isAdd} {info}");

                //发送变化事件
                _mediator.Publish(new ConnectionChangeNotification<IConnectionContext>(info, true));

                return 0;
            }
        }

        public void RemoveClientConnectionInfo(string connectionId, string iceProxyId = "")
        {
            lock (_lock)
            {
                try
                {
                    if (_clientsDic.TryGetValue(connectionId, out var info))
                    {
                        if (!iceProxyId.IsNullOrEmpty())
                        {
                            logger.LogWarning($"HeartBeat:RemoveClient 开始检测是否为新连接, {connectionId} Closed iceProxyId:{iceProxyId} ,Session  Proxyid:{info.IceProxyId}");

                            if (info.IceProxyId != iceProxyId)
                            {
                                logger.LogWarning($"HeartBeat:RemoveClient 完成检测，是新连接，不删除Session，直接返回,{info.toJsonStr()}");
                                return;
                            }
                            logger.LogWarning($"HeartBeat:RemoveClient 完成检测，不是新连接，删除Session");
                        }

                        logger.LogDebug($"HeartBeat:RemoveClient " + info.toJsonStr());

                        _clientsDic.TryRemove(connectionId, out _);

                        //发送变化事件
                        _mediator.Publish(new ConnectionChangeNotification<IConnectionContext>(info, false));
                    }
                    else
                    {
                        logger.LogDebug($"HeartBeat:RemoveClient _clientsDic 对应{connectionId}不存在,无需删除。");
                    }
                }
                catch (System.Exception ex)
                {
                    logger.LogError(ex, "RemoveClientConnectionInfo");
                }
            }
        }

        public void CloseClientById(string id, bool force = false)
        {
            if (_clientsDic.TryGetValue(id, out var info))
            {
                var result1 = (info.Connection.Invoker as IProtocolConnection)?.DisposeAsync().ConfigureAwait(false);
            }
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (!_clientsDic.IsEmpty)
                {
                    //断开连接
                    foreach (var info in _clientsDic.Values)
                    {
                        var result1 = (info.Connection.Invoker as IProtocolConnection)?.DisposeAsync();
                    }
                    _clientsDic.Clear();
                }
            }
        }

        public ClientConnectionInfo<IConnectionContext> GetClientConnectionByConnectId(string connectId)
        {
            if (_clientsDic.TryGetValue(connectId, out var info))
            {
                return info;
            }
            return null;
        }

        public ClientConnectionInfo<IConnectionContext> GetClientConnectionByConnectIdAndType(string idAndType)
        {
            lock (_lock)
            {
                string key = "";
                foreach (var info in _clientsDic.Values)
                {
                    key = $"{info.ClientId}:{info.ClientType}";
                    if (key == idAndType)
                    {
                        return info;
                    }
                }
                return null;
            }
        }

        public Dictionary<string, ClientConnectionInfo<IConnectionContext>> GetClientConnectionDic()
        {
            return _clientsDic.ToDictionary(a => a.Key, a => a.Value);
        }

        public List<ClientConnectionInfo<IConnectionContext>> GetClientConnectionList(ClientType type)
        {
            return _clientsDic.Values.Where(a => a.ClientType == type).ToList();
        }

        public bool PushStreamPackageListAsync(IEnumerable<StreamPackage> param, string msg, string connectionId, CancellationToken cancellationToken = default)
        {
            try
            {
                lock (_lockPush)
                {
                    //推送所有 
                    if (connectionId.IsNullOrEmpty())
                    {
                        foreach (var info in _clientsDic.Values)
                        {
                            var proxy = info.GetClientReceiverProxy();
                            proxy.PushStreamPackageListAsync(param, msg);
                        }
                        return true;
                    }
                    //推送某个客户端
                    if (!_clientsDic.TryGetValue(connectionId, out var info2))
                    {
                        return false;
                    }

                    var proxy2 = info2.GetClientReceiverProxy();
                    proxy2.PushStreamPackageListAsync(param, msg);
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "PushStreamPackageListAsync");
                return false;
            }
        }

    }
}
