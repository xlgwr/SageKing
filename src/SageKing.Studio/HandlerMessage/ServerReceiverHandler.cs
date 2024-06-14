using AntDesign;
using SageKing.IceRPC.EventMessage;
using SageKing.IceRPC.Extensions;
using SageKing.Studio.Data;
using SageKingIceRpc;
using System.Xml.Linq;

namespace SageKing.Studio.HandlerMessage;

public class ServerReceiverHandler(PackagesDataService packagesData) : IRequestHandler<ServerReceiverRequest, StreamPackage>
{
    public async Task<StreamPackage> Handle(ServerReceiverRequest request, CancellationToken cancellationToken)
    {
        if (packagesData.DataDic.TryGetValue(request.msgType, out var list))
        {
            list.Add(request.Packages);
        }
        else
        {
            packagesData.DataDic[request.msgType] = new List<StreamPackage[]> { request.Packages };
        }

        string desc = $"【{request.serverName}:{packagesData.ClientTypeDic.GetDesc(request.serverType)}】收到##$" +
            $"{request.msgType}:" +
            $"Packages:{request.Packages?.Length}";
        packagesData.NoticeAction?.Invoke($"服务端收到消息", desc, 0);

        await Task.CompletedTask;
        return 1000.GetStreamPackage(0, "ServerReceiverHandler 处理成功");
    }


}
