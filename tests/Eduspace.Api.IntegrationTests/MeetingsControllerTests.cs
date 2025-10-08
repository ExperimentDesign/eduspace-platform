using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Interfaces.REST;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Services;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Commands;

namespace Eduspace.Api.IntegrationTests.Controllers
{
    public class MeetingsControllerTests
    {
        [Fact]
        public async Task DeleteMeeting_Completa_RetornaOk()
        {
            var cmdSvc = new Mock<IMeetingCommandService>();
            var qrySvc = new Mock<IMeetingQueryService>();

            cmdSvc.Setup(s => s.Handle(It.IsAny<DeleteMeetingCommand>()))
                  .Returns(Task.CompletedTask);

            var controller = new MeetingsController(cmdSvc.Object, qrySvc.Object);

            var result = await controller.DeleteMeeting(123);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ok.StatusCode ?? 200);

            cmdSvc.Verify(s => s.Handle(It.IsAny<DeleteMeetingCommand>()), Times.Once);
        }

        [Fact]
        public async Task DeleteMeeting_SiServiceLanzaArgumentException_RetornaNotFound()
        {
            var cmdSvc = new Mock<IMeetingCommandService>();
            var qrySvc = new Mock<IMeetingQueryService>();

            cmdSvc.Setup(s => s.Handle(It.IsAny<DeleteMeetingCommand>()))
                  .ThrowsAsync(new ArgumentException("Meeting not found."));

            var controller = new MeetingsController(cmdSvc.Object, qrySvc.Object);

            var result = await controller.DeleteMeeting(999);

            var nf = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, nf.StatusCode ?? 404);
        }
    }
}
