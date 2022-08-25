using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.Repositories
{
    public interface IUserNotificationRepository
    {
        List<UserNotification> GetNewUserNotifications(string userId);
    }
}