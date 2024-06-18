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
    /// <typeparam name="TserverAddress">ServerAddress</typeparam>
    /// <typeparam name="Tpackage">StreamPackage</typeparam>
    public interface IServer<TserverAddress, Tpackage> : IPushPackage<Tpackage>, IDisposable
    {
        /// <summary>
        /// 服务类型
        /// </summary>
        public int ServerType { get; }

        public TserverAddress Listen();

        public Task ShutdownAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
