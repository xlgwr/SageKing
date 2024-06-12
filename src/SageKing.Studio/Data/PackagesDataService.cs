using IceRpc;
using Microsoft.Extensions.Options;
using SageKing.Core.Contracts;
using SageKing.IceRPC.Client.Options;
using SageKing.IceRPC.Extensions;
using SageKingIceRpc;

namespace SageKing.Studio.Data
{
    public class PackagesDataService
    {
        public readonly IClientConnectionProvider<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage, Pipeline, Identity> ClientConnectionProvider;

        public readonly ClientTypeDicOptions ClientTypeDic;
        public ConcurrentDictionary<string, List<StreamPackage[]>> DataDic;
        public ConcurrentDictionary<string, ClientConnectionInfo<IConnectionContext>> DataClientDic;

        public PackagesDataService(
            IClientConnectionProvider<IceRpc.ClientConnection, IceRPCClientOption, StreamPackage, Pipeline, Identity> clientConnectionProvider,
             IOptions<ClientTypeDicOptions> clientTypeDic
            )
        {
            this.ClientTypeDic = clientTypeDic.Value;
            this.DataDic = new ConcurrentDictionary<string, List<StreamPackage[]>>();
            this.DataClientDic = new ConcurrentDictionary<string, ClientConnectionInfo<IConnectionContext>>();
            this.ClientConnectionProvider = clientConnectionProvider;
        }

        public async Task<int> SendMsg(string msg, string serverName = "server1")
        {
            if (string.IsNullOrEmpty(msg))
            {
                return await Task.FromResult(0);
            }
            var connection = this.ClientConnectionProvider.GetClientConnection(serverName);
            var getdesc = ClientTypeDic.GetDesc(connection.ServerType);
            var result = await connection.SendStreamPackageListAsync(msg.GetDataStreamBody(), $"Test:Send->{serverName}[{getdesc}]");
            return await Task.FromResult(1);

        }

        public async Task<int> PushMsg(string msg, string connectionid)
        {
            if (string.IsNullOrEmpty(msg))
            {
                return await Task.FromResult(0);
            }
            if (DataClientDic.TryGetValue(connectionid, out var client))
            {
                var connection = client.GetClientReceiverProxy();
                var getdesc = ClientTypeDic.GetDesc(client.ClientType);
                var result = await connection.PushStreamPackageListAsync(msg.GetDataStreamBody(), $"Test:Push->{client.ClientId}[{getdesc}]");
            }
            return await Task.FromResult(1);

        }
    }
}
