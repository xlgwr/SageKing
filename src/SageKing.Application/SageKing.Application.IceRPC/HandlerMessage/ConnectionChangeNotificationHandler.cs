namespace SageKing.Application.IceRPC.HandlerMessage;

public class ConnectionChangeNotificationHandler(ISageKingPackagesService packagesData) : NotificationHandler<ConnectionChangeNotification<IConnectionContext>>
{
    protected override async void Handle(ConnectionChangeNotification<IConnectionContext> notification)
    {
        await packagesData.ClientConnectionChangeAsync(notification.isAdd, notification.ClientConnection);
    }
}
