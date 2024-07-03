namespace SageKing.Application.IceRPC.HandlerMessage;

public class ClientReceiverHandler(ISageKingPackagesService packagesData, IOptions<ClientTypeDicOptions> clientTypeDic) : IRequestHandler<ClientReceiverRequest, StreamPackage>
{
    /// <summary>
    /// 客户端配置参数
    /// </summary>
    private readonly ClientTypeDicOptions ClientTypeDic = clientTypeDic.Value;

    public async Task<StreamPackage> Handle(ClientReceiverRequest request, CancellationToken cancellationToken)
    {

        await packagesData.ReceiverMsgAsync(request.msgType, request.Packages);

        string desc = $"【{request.ClientName}:{ClientTypeDic.GetDesc(request.ClientType)}】收到##$" +
            $"{request.msgType}:" +
            $"Packages:{request.Packages?.Length}";

        packagesData.NoticeAction?.Invoke($"客户端收到消息", desc, 1); 

        return 1000.GetStreamPackage(0, "ClientReceiverHandler 处理成功");
    }
}
