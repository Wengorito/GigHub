using FluentAssertions;
using GigHub.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace GigHub.Tests.Core.Models
{
    [TestClass]
    public class GigTests
    {
        [TestMethod]
        public void Cancel_WhenCalled_ShouldSetIsCanceledToTrue()
        {
            var gig = new Gig();

            gig.Cancel();

            gig.IsCanceled.Should().BeTrue();
        }

        [TestMethod]
        public void Cancel_WhenCalled_ShouldAddNotificationToEachAttendee()
        {
            var gig = new Gig();

            var testNumber = 2;

            for (var i = 0; i < testNumber; i++)
            {
                gig.Attendances.Add(new Attendance { Attendee = new ApplicationUser() });
            }

            gig.Cancel();

            foreach (var attendee in gig.Attendances.Select(a => a.Attendee))
            {
                attendee.UserNotifications.Count.Should().Be(1);
            }
        }
    }
}