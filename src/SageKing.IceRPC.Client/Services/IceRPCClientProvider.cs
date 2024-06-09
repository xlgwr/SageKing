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
    internal class IceRPCClientProvider : IClientConnectionProvider
    {
        private IDictionary<string, IClientConnection> _dics;

        public IceRPCClientProvider(IServiceProvider serviceProvider, IOptions<List<IceRPCClientOption>> options)
        {
            _dics = new Dictionary<string, IClientConnection>();
            if (options == null || options.Value.Count <= 0)
            {
                return;
            }
            var scope = serviceProvider.CreateScope();
            foreach (var option in options.Value)
            {
                if (string.IsNullOrEmpty(option.Name))
                {
                    throw new ArgumentException(nameof(option), "Name");
                }
                var getScopeClient = scope.ServiceProvider.GetRequiredService<IceRPCClient>();
                if (getScopeClient != null)
                {
                    getScopeClient.InitClient(option);
                    _dics[option.Name] = getScopeClient;
                }
            }
        }
        public IClientConnection GetClientConnection(string name)
        {
            if (_dics.TryGetValue(name, out var client))
            {
                return client;
            }
            throw new InvalidOperationException($"{name} 不存在");
        }
    }
}
