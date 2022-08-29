using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IUserNotificationRepository
    {
        List<UserNotification> GetNewUserNotifications(string userId);
    }
}