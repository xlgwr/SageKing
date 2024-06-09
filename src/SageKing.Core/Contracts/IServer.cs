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
    public interface IServer<A, T> : IDisposable
    {
        public A Listen();

        public Task ShutdownAsync(CancellationToken cancellationToken = default(CancellationToken));

        public Task<T> PushStreamPackageListAsync(IEnumerable<T> param, string msg, string connectionId, CancellationToken cancellationToken = default(CancellationToken));

        public Task<T> PushStreamPackageListAsync(IEnumerable<T> param, string msg, CancellationToken cancellationToken = default(CancellationToken));
    }
}
