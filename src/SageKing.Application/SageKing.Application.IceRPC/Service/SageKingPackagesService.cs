using MediatR;

namespace SageKing.Application.IceRPC.Service;

public class SageKingPackagesService : ISageKingPackagesService
{
    /// <summary>
    /// 客户端配置参数
    /// </summary>
    private readonly ClientTypeDicOptions ClientTypeDic;

    /// <summary>
    /// 消息列表
    /// </summary>
    private ConcurrentDictionary<string, List<StreamPackage[]>> _messagesDic;

    /// <summary>
    /// 客户端连接列表
    /// </summary>
    private ConcurrentDictionary<string, ClientConnectionInfo<IConnectionContext>> _clientConnectionDic;

    /// <summary>
    /// 消息结构定义
    /// </summary>
    public ConcurrentDictionary<string, SageKingMessage> sageKingMessageDic;

    /// <summary>
    /// 服务端连接器
    /// </summary>
    private readonly IClientConnectionProvider<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage, Pipeline, Identity> ClientConnectionProvider;


    public SageKingPackagesService(
            IClientConnectionProvider<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage, Pipeline, Identity> clientConnectionProvider,
         IOptions<ClientTypeDicOptions> clientTypeDic
        )
    {
        this.ClientTypeDic = clientTypeDic.Value;
        this._messagesDic = new ConcurrentDictionary<string, List<StreamPackage[]>>();
        this._clientConnectionDic = new ConcurrentDictionary<string, ClientConnectionInfo<IConnectionContext>>();
        this.ClientConnectionProvider = clientConnectionProvider;
        this.sageKingMessageDic = new ConcurrentDictionary<string, SageKingMessage>();
    }

    public Action<string, string, int> NoticeAction { get; set; } = (a, b, type) => { };

    public ConcurrentDictionary<string, List<StreamPackage[]>> GetMessagesDic => _messagesDic;

    public List<string> GetServerNames => ClientConnectionProvider.GetServerNames().ToList();

    public ConcurrentDictionary<string, ClientConnectionInfo<IConnectionContext>> GetClientConnectionDic => _clientConnectionDic;

    public async Task<int> PushMsgAsync(string msg, string connectionid)
    {
        if (string.IsNullOrEmpty(msg))
        {
            return await Task.FromResult(0);
        }
        if (_clientConnectionDic.TryGetValue(connectionid, out var client))
        {
            var connection = client.GetClientReceiverProxy();
            var getdesc = ClientTypeDic.GetDesc(client.ClientType);
            var result = await connection.PushStreamPackageListAsync(msg.GetDataStreamBody(), $"Test:Push->{client.ClientId}[{getdesc}]");
        }
        return await Task.FromResult(1);
    }

    public async Task<int> SendMsgAsync(string msg, string serverName)
    {
        if (string.IsNullOrEmpty(msg))
        {
            return await Task.FromResult(0);
        }
        var connection = this.ClientConnectionProvider.GetClientConnection(serverName);
        var getdesc = ClientTypeDic.GetDesc(connection.ServerType);
        var result = await connection.SendStreamPackageListAsync(msg.GetDataStreamBody(), $"Test:Send->{serverName}[{getdesc}]");
        return await Task.FromResult(1);

    }

    public async Task<int> ReceiverMsgAsync(string msgType, StreamPackage[] streams)
    {
        if (_messagesDic.TryGetValue(msgType, out var list))
        {
            list.Add(streams);
        }
        else
        {
            _messagesDic[msgType] = new List<StreamPackage[]> { streams };
        }

        return await Task.FromResult(1);
    }

    public async Task<int> ClientConnectionChangeAsync(bool isAdd, ClientConnectionInfo<IConnectionContext> clientConnection)
    {
        if (isAdd)
        {
            _clientConnectionDic[clientConnection.ConnectionId] = clientConnection;
        }
        else
        {
            _clientConnectionDic.TryRemove(clientConnection.ConnectionId, out _);
        }
        return await Task.FromResult(1);
    }
}