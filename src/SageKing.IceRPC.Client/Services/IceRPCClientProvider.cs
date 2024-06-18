using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageKing.IceRPC.Client.Services
{
    public class IceRPCClientProvider : IClientConnectionProvider<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage, Pipeline, Identity>
    {
        private IDictionary<string, IClientConnection<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage, Pipeline, Identity>> _dics;

        public IceRPCClientProvider(IServiceProvider serviceProvider, IOptions<List<IceRPCClientOption>> options)
        {
            _dics = new Dictionary<string, IClientConnection<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage, Pipeline, Identity>>();
            if (options == null || options.Value.Count <= 0)
            {
                return;
            }
            var scope = serviceProvider.CreateScope();
            foreach (var option in options.Value)
            {
                if (string.IsNullOrEmpty(option.ServerName))
                {
                    throw new ArgumentException(nameof(option), "ServerName");
                }
                var getScopeClient = scope.ServiceProvider.GetRequiredService<IceRPCClient>();
                if (getScopeClient != null)
                {
                    getScopeClient.InitClient(option);
                    _dics[option.ServerName] = getScopeClient;
                }
            }
        }
        public IClientConnection<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage, Pipeline, Identity> GetClientConnection(string servername)
        {
            if (_dics.TryGetValue(servername, out var client))
            {
                return client;
            }
            throw new InvalidOperationException($"{servername} 不存在");
        }
        public IList<string> GetServerNames()
        {
            return _dics.Keys.ToList();
        }
    }
}
