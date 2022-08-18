using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        private string _userId;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
            _userId = User.Identity.GetUserId();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == _userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == _userId && !un.IsRead)
                .ToList();

            notifications.ForEach(un => un.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}
