
using IceRpc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Client.HostedServices;

/// <summary>
/// 启动 IceRPC Client 服务
/// </summary>
[UsedImplicitly]
public class IceRPCClientHostedService : IHostedService, IDisposable
{

    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, IClientConnection> _client;
    private readonly ILogger _logger;

    public IceRPCClientHostedService(IServiceProvider serviceProvider, ILoggerFactory loggerFactory, IOptions<List<IceRPCClientOption>> options)
    {
        _serviceProvider = serviceProvider;
        _logger = loggerFactory.CreateLogger<IceRPCClientHostedService>();

        using var scope = _serviceProvider.CreateScope();
        var instanceClientProvider = scope.ServiceProvider.GetRequiredService<IClientConnectionProvider>();

        _client = new Dictionary<string, IClientConnection>();
        foreach (var item in options.Value)
        {
            _client[item.Name] = instanceClientProvider.GetClientConnection(item.Name);
        }
    }

    public void Dispose()
    {
        foreach (var item in _client)
        {
            item.Value?.Dispose();
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"### StartAsync ConnectAsync->Clients:{_client.Count}");
        return Task.Run(async () =>
            {
                foreach (var item in _client)
                {
                    await item.Value!.ConnectAsync();
                    // Create a greeter proxy with this invocation pipeline.
                    var client = item.Value.Connection as ClientConnection;
                    var greeter = new ServerReceiverProxy(client!);

                    var greeting = await greeter.SendStreamPackageListAsync(Environment.UserName.GetDataStreamBody(), "send");

                    _logger.LogInformation($"### StartAsync ConnectAsync：{item.Key},greeting:{greeting.GetString()}");
                }
            });

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.Log(LogLevel.Information, $"StopAsync->ShutdownAsync->Clients:{_client.Count}");
        foreach (var item in _client)
        {
            item.Value!.ShutdownAsync();
            _logger.LogInformation($"### StopAsync->ShutdownAsync：{item.Key}");
        }
        return Task.CompletedTask;
    }
}
