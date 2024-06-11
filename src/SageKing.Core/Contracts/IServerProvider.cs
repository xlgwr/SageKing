using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

/// <summary>
/// 服务提供器
/// </summary>
/// <typeparam name="TserverAddress">ServerAddress</typeparam>
/// <typeparam name="Tpackage">StreamPackage</typeparam>
public interface IServerProvider<TserverAddress, Tpackage>
{
    public IServer<TserverAddress, Tpackage> GetServer();
}
