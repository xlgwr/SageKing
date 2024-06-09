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
using MyClient;


namespace SageKing.IceRPC.Server.Services;

public class IceRPCServer : IServer<ServerAddress>
{
    private IceRpc.Server _server;
    private readonly IceRPCServerOption _ServerOption;

    public IceRPCServer(IceRPCServerOption iceRPCServerOption)
    {
        this._ServerOption = iceRPCServerOption;

        // Create a simple console logger factory and configure the log level for category IceRpc.
        using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            builder
                .AddSimpleConsole()
                .AddFilter("IceRpc", LogLevel.Information));

        // Create a router (dispatch pipeline), install two middleware and map our implementation of `IGreeterService` at the
        // default path for this interface: `/VisitorCenter.Greeter`
        Router router = new Router()
            .UseLogger(loggerFactory)
            .UseDeadline()
            .Map<IGreeterService>(new Chatbot());

        // Create a server that logs message to a logger with category `IceRpc.Server`.
        var sslServerAuthenticationOptions = new SslServerAuthenticationOptions
        {
            ServerCertificate = new X509Certificate2(_ServerOption.ServerCertificateFileName)
        };

        this._server = new IceRpc.Server(
         router,
         new Uri(_ServerOption.ServerAddress),
         sslServerAuthenticationOptions,
         multiplexedServerTransport: new QuicServerTransport(),
         logger: loggerFactory.CreateLogger<IceRpc.Server>()); 

        Console.WriteLine("Listen Start.");
    }

    public ServerAddress Listen()
    {
        return _server!.Listen();
    }

    public Task ShutdownAsync(CancellationToken cancellationToken = default)
    {
        return _server!.ShutdownAsync(cancellationToken);
    }
}
