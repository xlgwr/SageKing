
using SageKing.IceRPC.Extensions;
using SageKing.Studio.Data;
using SageKingIceRpc;

namespace SageKing.Studio.HandlerMessage;

public class ConnectionChangeNotificationHandler(PackagesDataService packagesData) : NotificationHandler<ConnectionChangeNotification<IConnectionContext>>
{
    protected override void Handle(ConnectionChangeNotification<IConnectionContext> notification)
    {
        if (notification.isAdd)
        {
            packagesData.DataClientDic[notification.ClientConnection.ConnectionId] = notification.ClientConnection;
        }
        else
        {
            packagesData.DataClientDic.TryRemove(notification.ClientConnection.ConnectionId, out _);
        }
    }
}
