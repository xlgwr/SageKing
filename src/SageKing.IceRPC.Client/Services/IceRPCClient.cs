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
using SageKing.IceRPC.Client.Services.SliceService;

namespace SageKing.IceRPC.Client.Services
{
    public class IceRPCClient(ClientReceiver clientReceiver, ILoggerFactory loggerFactory) : IClientConnection<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage, Pipeline>
    {
        private IceRpc.ClientConnection _client;
        private Pipeline _pipeline;
        private IceRPCClientOption _clientOption;

        public IceRpc.ClientConnection Connection { get => _client; }

        public IceRPCClientOption Options { get => _clientOption; }

        public Pipeline Pipeline { get => _pipeline; }

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

            //router
            Router router = new Router()
                .UseLogger(loggerFactory)
                .UseRequestContext()
                .UseDispatchInformation()
                .UseMetrics()
                .Map<IClientReceiverService>(clientReceiver);

            // Path to the root CA certificate.
            using var rootCA = new X509Certificate2(_clientOption.ServerCertificateFileName);

            // Create Client authentication options with custom certificate validation.
            var clientAuthenticationOptions = new SslClientAuthenticationOptions
            {
                RemoteCertificateValidationCallback = rootCA.CreateCustomRootRemoteValidator()
            };

            //option
            var clientOption = new ClientConnectionOptions
            {
                ServerAddress = new ServerAddress(new Uri(_clientOption.ServerAddress)),
                Dispatcher = router,
                ClientAuthenticationOptions = clientAuthenticationOptions
            };

            // Create a client connection that logs messages to a logger with category IceRpc.ClientConnection.
            _client = new ClientConnection(
                clientOption,
                multiplexedClientTransport: new QuicClientTransport(),
                logger: loggerFactory.CreateLogger<ClientConnection>());

            // Create an invocation pipeline with two interceptors.
            _pipeline = new Pipeline()
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
