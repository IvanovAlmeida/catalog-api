using Catalog.Domain.Notifications;
using System.Collections.Generic;

namespace Catalog.Domain.Interfaces
{
    public interface INotificator
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
