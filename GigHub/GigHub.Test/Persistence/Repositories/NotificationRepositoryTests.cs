using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using GigHub.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;

namespace GigHub.Test.Persistence.Repositories
{
    [TestClass]
    public class NotificationRepositoryTests
    {
        private NotificationRepository _repository;
        private Mock<DbSet<UserNotification>> _mockUserNotifications;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUserNotifications = new Mock<DbSet<UserNotification>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.UserNotifications).Returns(_mockUserNotifications.Object);

            _repository = new NotificationRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetNewNotifications_NoNotificationsForGivenUser_ShouldNotBeReturned()
        {
            var user = new ApplicationUser();
            var notification = Notification.GigCancelled(new Gig());
            var userNotification = new UserNotification(user, notification);

            _mockUserNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetNewNotifications(user.Id + "-");

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotifications_NoUnreadNotificationsForGivenUser_ShouldNotBeReturned()
        {
            var user = new ApplicationUser();
            var notification = Notification.GigCancelled(new Gig());
            var userNotification = new UserNotification(user, notification);
            userNotification.Read();

            _mockUserNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetNewNotifications(user.Id);

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotifications_UnreadNotificationForGivenUserExists_ShouldBeReturned()
        {
            var user = new ApplicationUser();
            var notification = Notification.GigCancelled(new Gig());
            var userNotification = new UserNotification(user, notification);

            _mockUserNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetNewNotifications(user.Id);

            notifications.Should().Contain(notification);
        }
    }
}
