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
using SageKing.IceRPC.Client.Extensions;

namespace SageKing.IceRPC.Client.Services
{
    public class IceRPCClient(ILoggerFactory loggerFactory) : IClientConnection<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage>
    {
        private IceRpc.ClientConnection _client;
        private IceRPCClientOption _clientOption;

        public IceRpc.ClientConnection Connection { get => _client; }

        public Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            return _client.ConnectAsync(cancellationToken);
        }

        public void Dispose()
        {
            _client?.DisposeAsync();
        }

        public void InitClient(IceRPCClientOption options)
        {
            if (options == null)
            {
                throw new ArgumentException(nameof(options));
            }
            _clientOption = options;

            // Create a simple console logger factory and configure the log level for category IceRpc.
            //using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            //    builder
            //        .AddSimpleConsole()
            //        .AddFilter("IceRpc", LogLevel.Information));

            // Path to the root CA certificate.
            using var rootCA = new X509Certificate2(_clientOption.ServerCertificateFileName);

            // Create Client authentication options with custom certificate validation.
            var clientAuthenticationOptions = new SslClientAuthenticationOptions
            {
                RemoteCertificateValidationCallback = rootCA.CreateCustomRootRemoteValidator()
            };

            // Create a client connection that logs messages to a logger with category IceRpc.ClientConnection.
            _client = new ClientConnection(
                new Uri(_clientOption.ServerAddress),
                clientAuthenticationOptions,
                multiplexedClientTransport: new QuicClientTransport(),
                logger: loggerFactory.CreateLogger<ClientConnection>());

            // Create an invocation pipeline with two interceptors.
            Pipeline pipeline = new Pipeline()
                .UseLogger(loggerFactory)
                .UseDeadline(_clientOption.Timeout)
                .Into(_client);
        }

        public async Task<StreamPackage> SendStreamPackageListAsync(IEnumerable<StreamPackage> param, string msg, CancellationToken cancellationToken = default)
        {
            var serverReceiver = new ServerReceiverProxy(_client!);
            return await serverReceiver.SendStreamPackageListAsync(param, msg, cancellationToken: cancellationToken);
        }

        public Task ShutdownAsync(CancellationToken cancellationToken = default)
        {
            if (_client != null)
            {
                return _client.ShutdownAsync(cancellationToken);
            }
            return Task.CompletedTask;
        }
    }
}
