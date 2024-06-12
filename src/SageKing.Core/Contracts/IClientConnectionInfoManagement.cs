using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TconnContext">IConnectionContext</typeparam>
/// <typeparam name="Tpackage">StreamPackage</typeparam>
public interface IClientConnectionInfoManagement<TconnContext, Tpackage, Tidentity> : IPushPackage<Tpackage>, IDisposable
{

    public List<ClientConnectionInfo<TconnContext>> GetClientConnectionList(int clienttype);

    public Dictionary<string, ClientConnectionInfo<TconnContext>> GetClientConnectionDic();

    public ClientConnectionInfo<TconnContext> GetClientConnectionByConnectId(string connectId);

    /// <summary>
    /// $"{info.ClientId}:{info.ClientType}"
    /// </summary>
    /// <param name="idAndType"></param>
    /// <returns></returns>
    public ClientConnectionInfo<TconnContext> GetClientConnectionByConnectIdAndType(string idAndType);

    public int AddClientConnectionInfo(Tidentity identity, EndPoint remteAddress, TconnContext conn, string iceProxyId);

    public void RemoveClientConnectionInfo(string connectionId, string iceProxyId = "");

    /// <summary>
    /// 主动断开客户连接
    /// </summary>
    /// <param name="id"></param>
    /// <param name="force"></param>
    public void CloseClientById(string id, bool force = false);

}
