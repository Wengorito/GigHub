using GigHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public UserNotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserNotification> GetNewUserNotifications(string userId)
        {
            return _context.UserNotifications.Where(un => un.UserId == userId && !un.IsRead).ToList();
        }
    }
}