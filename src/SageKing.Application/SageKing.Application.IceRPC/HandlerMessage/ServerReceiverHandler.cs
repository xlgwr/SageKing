namespace SageKing.Application.IceRPC.HandlerMessage;

public class ServerReceiverHandler(ISageKingPackagesService packagesData, IOptions<ClientTypeDicOptions> clientTypeDic) : IRequestHandler<ServerReceiverRequest, StreamPackage>
{
    /// <summary>
    /// 客户端配置参数
    /// </summary>
    private readonly ClientTypeDicOptions ClientTypeDic = clientTypeDic.Value;

    public async Task<StreamPackage> Handle(ServerReceiverRequest request, CancellationToken cancellationToken)
    {
        await packagesData.ReceiverMsgAsync(request.msgType, request.Packages);

        string desc = $"【{request.serverName}:{ClientTypeDic.GetDesc(request.serverType)}】收到##$" +
            $"{request.msgType}:" +
            $"Packages:{request.Packages?.Length}";

        packagesData.NoticeAction?.Invoke($"服务端收到消息", desc, 0); 

        return 1000.GetStreamPackage(0, "ServerReceiverHandler 处理成功");
    }


}
