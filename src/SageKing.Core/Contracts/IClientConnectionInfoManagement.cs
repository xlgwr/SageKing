using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T">IConnectionContext</typeparam>
public interface IClientConnectionInfoManagement<T, B> : IPushPackage<B>, IDisposable
{

    public List<ClientConnectionInfo<T>> GetClientConnectionList(ClientType type);

    public Dictionary<string, ClientConnectionInfo<T>> GetClientConnectionDic();

    public ClientConnectionInfo<T> GetClientConnectionByConnectId(string connectId);

    /// <summary>
    /// $"{info.ClientId}:{info.ClientType}"
    /// </summary>
    /// <param name="idAndType"></param>
    /// <returns></returns>
    public ClientConnectionInfo<T> GetClientConnectionByConnectIdAndType(string idAndType);

    public int AddClientConnectionInfo(string clientId, string connectionId, EndPoint remteAddress, int type, T conn, string iceProxyId);

    public void RemoveClientConnectionInfo(string connectionId, string iceProxyId = "");

    /// <summary>
    /// 主动断开客户连接
    /// </summary>
    /// <param name="id"></param>
    /// <param name="force"></param>
    public void CloseClientById(string id, bool force = false);

}
