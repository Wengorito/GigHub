using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTests
    {
        private Mock<IAttendanceRepository> _mockRepository;
        private AttendancesController _controller;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Attendances).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Attend_UserAlreadyAttending_ShouldReturnBadRequest()
        {
            _mockRepository.Setup(r => r.GetAttendance(_userId, 1)).Returns(new Attendance());

            var result = _controller.Attend(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            var result = _controller.Attend(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<OkResult>();
        }

        // Missing in the course IMO
        public void Attend_GigNonExistent_ShouldReturnNotFound()
        {
        }

        public void Attend_GigDateBeforeToday_ShouldReturnNotFound()
        {
        }

        public void Attend_GigIsCancelled_ShouldReturnBadRequest()
        {
        }

        [TestMethod]
        public void CancelAttendance_NoAttendanceWithGivenId_ShouldReturnNotFound()
        {
            var result = _controller.CancelAttendance(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void CancelAttendance_CancellingOtherUserAttendance_ShouldReturnUnauthorised()
        {
            _mockRepository.Setup(r => r.GetAttendance(_userId, 1)).Returns(new Attendance { AttendeeId = _userId + "-" });

            var result = _controller.CancelAttendance(1);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void CancelAttendance_ValidRequest_ShouldReturnOk()
        {
            _mockRepository.Setup(r => r.GetAttendance(_userId, 1)).Returns(new Attendance { AttendeeId = _userId });

            var result = _controller.CancelAttendance(1);

            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }

        [TestMethod]
        public void CancelAttendance_ValidRequest_ShouldReturnDeletedAttendanceGigId()
        {
            _mockRepository.Setup(r => r.GetAttendance(_userId, 1)).Returns(new Attendance { AttendeeId = _userId });

            var result = (OkNegotiatedContentResult<int>)_controller.CancelAttendance(1);

            result.Content.Should().Be(1);
        }
    }
}
