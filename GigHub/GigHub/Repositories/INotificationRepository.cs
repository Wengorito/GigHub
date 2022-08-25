using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNewNotifications(string userId);
    }
}