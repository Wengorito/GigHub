using FluentAssertions;
using GigHub.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GigHub.Tests.Core.Models
{
    [TestClass]
    public class ApplicationUserTests
    {
        [TestMethod]
        public void Notify_WhenCalled_ShouldAddUserNotificationToUsersCollection()
        {
            var applicationUser = new ApplicationUser();
            var notification = Notification.GigCancelled(new Gig());

            applicationUser.Notify(notification);


            // 3 assertions in one test - supposedly OK because are related
            applicationUser.UserNotifications.Count.Should().Be(1);

            var userNotification = applicationUser.UserNotifications.First();
            userNotification.Notification.Should().Be(notification);
            userNotification.User.Should().Be(applicationUser);

            // From the course author:

            // We have 3 assertions here, but this does not mean this test method
            // is violating the single responibility principle. These 3 assertions
            // are highly related and we're logically verifying one fact: that
            // the user object will have one UserNotification object in the right
            // state (meaning its User and Notification properties are set properly).
        }
    }
}
