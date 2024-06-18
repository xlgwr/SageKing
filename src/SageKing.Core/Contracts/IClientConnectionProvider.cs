using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

/// <summary>
/// 
/// </summary>
/// <typeparam name="Tclientconn">ClientConnection</typeparam>
/// <typeparam name="Toption">IceRPCClientOption</typeparam>
/// <typeparam name="Tpackage">StreamPackage</typeparam>
/// <typeparam name="Tpipe">Pipeline</typeparam>
public interface IClientConnectionProvider<Tclientconn, Toption, Tpackage, Tpipe, TIdentity>
{
    /// <summary>
    /// 获取服务连接
    /// </summary>
    /// <param name="servername"></param>
    /// <returns></returns>
    public IClientConnection<Tclientconn, Toption, Tpackage, Tpipe, TIdentity> GetClientConnection(string servername);


    /// <summary>
    /// 获取服务端列表
    /// </summary>
    /// <returns></returns>
    public IList<string> GetServerNames();
}
