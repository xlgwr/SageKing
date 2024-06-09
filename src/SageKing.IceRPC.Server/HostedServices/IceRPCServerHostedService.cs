
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
    private readonly ILogger _logger;

    public IceRPCServerHostedService(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
    {
        _serviceProvider = serviceProvider;
        _logger = loggerFactory.CreateLogger<IceRPCServerHostedService>();
         
        var scope = _serviceProvider.CreateScope();
        var instanceServerProvider = scope.ServiceProvider.GetRequiredService<IServerProvider<ServerAddress>>();
        _server = instanceServerProvider.GetServer();
    }

    public void Dispose()
    {
        _server?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            var address = _server!.Listen();
            _logger.LogInformation($"### StartAsync Listen：{address}");
        });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.Log(LogLevel.Information, "StopAsync->ShutdownAsync");
        return _server!.ShutdownAsync(cancellationToken);
    }
}
