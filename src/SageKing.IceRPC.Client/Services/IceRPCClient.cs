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
using SageKing.Core.Options;
using SageKing.IceRPC.IceFeatures;

namespace SageKing.IceRPC.Client.Services
{
    public class IceRPCClient(ClientReceiver clientReceiver, ILoggerFactory loggerFactory) : IClientConnection<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage, Pipeline, Identity>
    {
        private Pipeline _pipeline;
        private Identity _Identity;
        private IceRPCClientOption _option;
        private IceRpc.ClientConnection _client;

        public IceRpc.ClientConnection Connection { get => _client; }

        public IceRPCClientOption Options { get => _option; }

        public Pipeline Pipeline { get => _pipeline; }

        public Identity  Identity { get => _Identity; }

        public Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            return _client.ConnectAsync(cancellationToken);
        }

        public void Dispose()
        {
            _client?.DisposeAsync();
        }

        public Identity GetIdentity()
        {
            throw new NotImplementedException();
        }

        public void InitClient(IceRPCClientOption option)
        {
            if (option == null)
            {
                throw new ArgumentException(nameof(option));
            }
            _option = option;

            //router
            var router = new Router()
                .UseLogger(loggerFactory)
                .UseDispatchInformation();

            #region use set
            if (_option.UseCompressor)
            {
                router = router.UseCompressor(CompressionFormat.Brotli);
            }
            if (_option.UseMetrics)
            {
                router = router.UseMetrics();
            }
            if (_option.UseRequestContext)
            {
                router = router.UseRequestContext();
            }
            #endregion

            router = router.Map<IClientReceiverService>(clientReceiver);

            //option
            var clientOption = new ClientConnectionOptions
            {
                ServerAddress = new ServerAddress(new Uri(_option.ServerAddress)),
                Dispatcher = router
            };

            if (_option.IsQuic || _option.IsTcpTLS)
            {
                // Path to the root CA certificate.
                var rootCA = new X509Certificate2(_option.CertificateFileName);

                // Create Client authentication options with custom certificate validation.
                var clientAuthenticationOptions = new SslClientAuthenticationOptions
                {
                    RemoteCertificateValidationCallback = rootCA.CreateCustomRootRemoteValidator()
                };

                clientOption.ClientAuthenticationOptions = clientAuthenticationOptions;
            }

            // Create a client connection that logs messages to a logger with category IceRpc.ClientConnection.
            if (_option.IsQuic)
            {
                _client = new ClientConnection(
                                            clientOption,
                                            multiplexedClientTransport: new QuicClientTransport(),
                                            logger: loggerFactory.CreateLogger<ClientConnection>()
                );
            }
            else if (_option.IsTcpTLS)
            {
                _client = new ClientConnection(
                                            clientOption,
                                            logger: loggerFactory.CreateLogger<ClientConnection>()
                );
            }
            else
            {
                _client = new ClientConnection(
                                           clientOption,
                                           logger: loggerFactory.CreateLogger<ClientConnection>()
               );
            }

            // Create an invocation pipeline with two interceptors.
            _pipeline = new Pipeline()
                .UseLogger(loggerFactory)
                .UseDeadline(TimeSpan.FromSeconds(_option.Timeout))
                .Into(_client);
            
            //生成身份信息
            _Identity = new Identity()
            {
                Guid = Guid.NewGuid().ToString("N"),
                Name = _option.ClientId,
                Type = _option.ClientType
            };

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
