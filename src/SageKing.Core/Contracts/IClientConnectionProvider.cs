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
/// <typeparam name="L">Pipeline</typeparam>
public interface IClientConnectionProvider<C, P, T, L>
{
    public IClientConnection<C, P, T, L> GetClientConnection(string servername);

}
