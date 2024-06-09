using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.Core.Contracts;

public interface IClientConnection : IDisposable
{
    public object Connection { get; }
    public void InitClient(object options);
    public Task ConnectAsync(CancellationToken cancellationToken = default(CancellationToken));

    public Task ShutdownAsync(CancellationToken cancellationToken = default(CancellationToken));
}
