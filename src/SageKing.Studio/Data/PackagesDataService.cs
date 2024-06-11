using IceRpc;
using SageKing.Core.Contracts;
using SageKing.IceRPC.Client.Options;
using SageKing.IceRPC.Extensions;
using SageKingIceRpc;

namespace SageKing.Studio.Data
{
    public class PackagesDataService
    {
        private readonly IClientConnectionProvider<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage, Pipeline, Identity> _clientConnectionProvider;

        public ConcurrentDictionary<string, List<StreamPackage[]>> dataDic;
        public ConcurrentDictionary<string, ClientConnectionInfo<IConnectionContext>> dataClientDic;

        public PackagesDataService(IClientConnectionProvider<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage, Pipeline, Identity> clientConnectionProvider)
        {
            dataDic = new ConcurrentDictionary<string, List<StreamPackage[]>>();
            dataClientDic = new ConcurrentDictionary<string, ClientConnectionInfo<IConnectionContext>>();
            _clientConnectionProvider = clientConnectionProvider;
        }

        public async Task<int> SendMsg(string msg, string serverName = "server1")
        {
            if (string.IsNullOrEmpty(msg))
            {
                return await Task.FromResult(0);
            }
            var connection = _clientConnectionProvider.GetClientConnection(serverName);
            var result = await connection.SendStreamPackageListAsync(msg.GetDataStreamBody(), "Test:Send");
            return await Task.FromResult(1);

        }

        public async Task<int> PushMsg(string msg, string connectionid)
        {
            if (string.IsNullOrEmpty(msg))
            {
                return await Task.FromResult(0);
            }
            if (dataClientDic.TryGetValue(connectionid, out var client))
            {
                var connection = client.GetClientReceiverProxy();
                var result = await connection.PushStreamPackageListAsync(msg.GetDataStreamBody(), $"Test:Push->{client.ClientId}");
            }
            return await Task.FromResult(1);

        }
    }
}
