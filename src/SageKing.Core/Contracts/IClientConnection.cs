using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

public interface IClientConnection<C, P, T> : IDisposable
{
    public C Connection { get; }
    public void InitClient(P options);
    public Task ConnectAsync(CancellationToken cancellationToken = default(CancellationToken));

    public Task ShutdownAsync(CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="param"></param>
    /// <param name="msg"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> SendStreamPackageListAsync(IEnumerable<T> param, string msg, CancellationToken cancellationToken = default(CancellationToken));
}
