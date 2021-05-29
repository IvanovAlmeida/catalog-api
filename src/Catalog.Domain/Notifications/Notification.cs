using System;

namespace Catalog.Domain.Notifications
{
    public class Notification
    {
        public Notification(string message)
        {
            Message = message;
            Timestamp = DateTime.Now;
        }

        public string Message { get; }
        public DateTime Timestamp { get; }
    }
}
