using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

/// <summary>
/// 
/// </summary>
/// <typeparam name="C">ClientConnection</typeparam>
/// <typeparam name="P">IceRPCClientOption</typeparam>
/// <typeparam name="T">StreamPackage</typeparam>
public interface IClientConnectionProvider<C, P, T>
{
    public IClientConnection<C, P, T> GetClientConnection(string servername);

}
