
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Server.HostedServices;

/// <summary>
/// 启动 IceRPC Server 服务
/// </summary>
[UsedImplicitly]
public class IceRPCServerHostedService : IHostedService, IDisposable
{

    private readonly IServiceProvider _serviceProvider;
    private readonly IServer<ServerAddress> _server;

    public IceRPCServerHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;


        using var scope = _serviceProvider.CreateScope();
        var instanceServerProvider = scope.ServiceProvider.GetRequiredService<IServerProvider<ServerAddress>>();
        _server = instanceServerProvider.GetServer();
    }

    public void Dispose()
    {

    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() => { _server!.Listen(); });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return _server!.ShutdownAsync(cancellationToken);
    }
}
