
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

    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, IClientConnection<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage>> _clientDic;

    public IceRPCClientHostedService(IServiceProvider serviceProvider, ILoggerFactory loggerFactory, IOptions<List<IceRPCClientOption>> options)
    {
        _serviceProvider = serviceProvider;
        _logger = loggerFactory.CreateLogger<IceRPCClientHostedService>();
        _clientDic = new Dictionary<string, IClientConnection<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage>>();

        var scope = _serviceProvider.CreateScope();
        var instanceClientProvider = scope.ServiceProvider.GetRequiredService<IClientConnectionProvider<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage>>();

        foreach (var item in options.Value)
        {
            _clientDic[item.ServerName] = instanceClientProvider.GetClientConnection(item.ServerName);
        }
    }

    public void Dispose()
    {
        foreach (var item in _clientDic)
        {
            item.Value?.Dispose();
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"### StartAsync ConnectAsync->Clients:{_clientDic.Count}");

        return Task.Run(async () =>
            {
                foreach (var item in _clientDic)
                {
                    // 
                    await item.Value!.ConnectAsync();

                    var client = item.Value.Connection as ClientConnection;
                    var proxy = new ServerReceiverProxy(client!);

                    var ident = new Identity() { Category = item.Value.Options.ClientId, Name = Guid.NewGuid().ToString("N") };

                    //注册客户端
                    var reg = await proxy.RegClientAsync(ident, item.Value.Options.ClientType);

                    //发送hello
                    var result = await proxy.SendStreamPackageListAsync($"Hello:Server,My Name->{Environment.UserName}".GetDataStreamBody(), "send");

                    _logger.LogInformation($"### StartAsync ConnectAsync：{item.Key},greeting:{result.GetString()}");
                }
            });

    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.Log(LogLevel.Information, $"StopAsync->ShutdownAsync->Clients:{_clientDic.Count}");

        foreach (var item in _clientDic)
        {
            item.Value!.ShutdownAsync();

            _logger.LogInformation($"### StopAsync->ShutdownAsync：{item.Key}");
        }

        return Task.CompletedTask;
    }
}
