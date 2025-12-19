namespace Heroes.Common.Util.Services;

public class NotificationHandler : INotificationHandler<Notification>
{
    #region properties

    private IList<Notification> _notifications;
    public bool HasNotifications => _notifications.Any();

    #endregion

    #region Constructor

    public NotificationHandler() => _notifications = new List<Notification>();

    #endregion

    #region Methods

    public Task Handle(Notification notification)
    {
        _notifications.Add(notification);

        return Task.CompletedTask;
    }

    public virtual IList<Notification> GetNotifications() => _notifications;

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion
}