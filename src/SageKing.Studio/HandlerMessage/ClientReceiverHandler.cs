using SageKing.IceRPC.EventMessage;
using SageKing.IceRPC.Extensions;
using SageKing.Studio.Data;
using SageKingIceRpc;

namespace SageKing.Studio.HandlerMessage;

public class ClientReceiverHandler(PackagesDataService packagesData) : IRequestHandler<ClientReceiverRequest, StreamPackage>
{
    public async Task<StreamPackage> Handle(ClientReceiverRequest request, CancellationToken cancellationToken)
    {
        if (packagesData.dataDic.TryGetValue(request.msgType, out var list))
        {
            list.Add(request.Packages);
        }
        else
        {
            packagesData.dataDic[request.msgType] = new List<StreamPackage[]> { request.Packages };
        }
        await Task.CompletedTask;
        return 1000.GetStreamPackage(0, "ClientReceiverHandler 处理成功");
    }
}
