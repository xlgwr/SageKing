using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IceRpc.Slice;
using IceRpc.Transports.Quic;
using Microsoft.Extensions.Logging;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Options;


namespace SageKing.IceRPC.Server.Services;

public class IceRPCServer : IServer<ServerAddress, StreamPackage>
{
    private IceRpc.Server _server;
    private readonly ILogger _logger;
    private readonly IceRPCServerOption _ServerOption;
    private readonly ClientConnectionInfoManagement _connectionInfoManagement;

    public IceRPCServer(
        IOptions<IceRPCServerOption> iceRPCServerOption,
        ClientConnectionInfoManagement connectionInfoManagement,
        ServerReceiver serverReceiver,
        ILoggerFactory loggerFactory)
    {

        _ServerOption = iceRPCServerOption.Value;
        _logger = loggerFactory.CreateLogger<IceRpc.Server>();
        _connectionInfoManagement = connectionInfoManagement;

        // Create a simple console logger factory and configure the log level for category IceRpc.
        //using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        //    builder
        //        .AddSimpleConsole()
        //        .AddFilter("IceRpc", LogLevel.Information));

        // Create a router (dispatch pipeline), install two middleware and map our implementation of `IGreeterService` at the
        // default path for this interface: `/VisitorCenter.Greeter`
        Router router = new Router()
            .UseLogger(loggerFactory)
            .UseDeadline()
            .UseDispatchInformation()
            .UseMetrics()
            .UseRequestContext()
            .Map<IServerReceiverService>(serverReceiver);

        // Create a server that logs message to a logger with category `IceRpc.Server`.
        var sslServerAuthenticationOptions = new SslServerAuthenticationOptions
        {
            ServerCertificate = new X509Certificate2(_ServerOption.ServerCertificateFileName)
        };

        _server = new IceRpc.Server(
           router,
           new Uri(_ServerOption.ServerAddress),
           sslServerAuthenticationOptions,
           multiplexedServerTransport: new QuicServerTransport(),
           logger: _logger);
    }

    public ServerAddress Listen()
    {
        return _server!.Listen();
    }

    public Task ShutdownAsync(CancellationToken cancellationToken = default)
    {
        if (_server != null)
        {
            return _server.ShutdownAsync(cancellationToken);
        }
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _server?.DisposeAsync();
    }

    public bool PushStreamPackageListAsync(IEnumerable<StreamPackage> param, string msg, string connectionId, CancellationToken cancellationToken = default)
    {
        return _connectionInfoManagement.PushStreamPackageListAsync(param, msg, connectionId, cancellationToken);
    }
}
