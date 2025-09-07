using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Services;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Interfaces.REST.Resources;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FULLSTACKFURY.EduSpace.API.ReservationScheduling.Interfaces.REST;

[ApiController]
[Route("api/v1/meetings/{meetingId:int}/teachers/{teacherId:int}")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Meetings")]
public class MeetingParticipantsController(IMeetingCommandService commandService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddTeacherToMeeting([FromRoute]int meetingId, [FromRoute] int teacherId)
    {
        var addTeacherToMeetingResource= new AddTeacherToMeetingResource(teacherId, meetingId);
        var addTeacherToMeetingCommand = AddTeacherToMeetingCommandFromResourceAssembler
            .ToCommandFromResource(addTeacherToMeetingResource);
        await commandService.Handle(addTeacherToMeetingCommand);
        return Ok("Teacher added to meeting.");
    }
}