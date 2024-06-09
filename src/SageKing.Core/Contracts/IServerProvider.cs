using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

/// <summary>
/// 服务提供器
/// </summary>
public interface IServerProvider<T>
{
    public IServer<T> GetServer();
}
