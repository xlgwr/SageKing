using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts
{
    /// <summary>
    /// 服务端接口
    /// </summary>
    /// <typeparam name="A">ServerAddress</typeparam>
    /// <typeparam name="T">StreamPackage</typeparam>
    public interface IServer<A, T> : IPushPackage<T>, IDisposable
    {
        public A Listen();

        public Task ShutdownAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
