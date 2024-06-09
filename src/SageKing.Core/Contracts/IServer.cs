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
    public interface IServer<T> : IDisposable
    {
        public T Listen();

        public Task ShutdownAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
