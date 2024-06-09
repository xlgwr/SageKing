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

public class IceRPCServer : IServer<ServerAddress>
{
    private IceRpc.Server _server;
    private readonly IceRPCServerOption _ServerOption;
    private readonly ILogger _logger;

    public IceRPCServer(IOptions<IceRPCServerOption> iceRPCServerOption, ServerReceiver serverReceiver, ILoggerFactory loggerFactory)
    {
        this._ServerOption = iceRPCServerOption.Value;

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
            .Map<IServerReceiverService>(serverReceiver);

        // Create a server that logs message to a logger with category `IceRpc.Server`.
        var sslServerAuthenticationOptions = new SslServerAuthenticationOptions
        {
            ServerCertificate = new X509Certificate2(_ServerOption.ServerCertificateFileName)
        };

        _logger = loggerFactory.CreateLogger<IceRpc.Server>();

        this._server = new IceRpc.Server(
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

    public Task<T> PushStreamPackageListAsync<T>(List<T> param, string msg, string connectionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<T> PushStreamPackageListAsync<T>(List<T> param, string msg, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
