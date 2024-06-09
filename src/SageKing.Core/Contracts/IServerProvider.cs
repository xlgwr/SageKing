using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

/// <summary>
/// 服务提供器
/// </summary>
/// <typeparam name="A">ServerAddress</typeparam>
/// <typeparam name="T">StreamPackage</typeparam>
public interface IServerProvider<A, T>
{
    public IServer<A, T> GetServer();
}
