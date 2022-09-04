using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Test.Extensions;
using NUnit.Framework;
using System;
using System.Linq;

namespace GigHub.IntegrationTests.Controllers.Api
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Cancel_WhenCalled_ShouldAddUserNotifcation()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();
            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            gig.Attendances.Add(new Attendance { Gig = gig, Attendee = user });
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            // Act
            _controller.Cancel(gig.Id);

            // Assert
            var result = _context.UserNotifications.ToList();
            result.Should().HaveCount(1);
            result.First().Notification.Type.Should().Be(NotificationType.GigCancelled);
        }
    }
}
