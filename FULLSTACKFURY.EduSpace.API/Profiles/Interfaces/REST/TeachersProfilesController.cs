using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Services;
using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.REST.Resources;
using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/teachers-profiles")]
[Produces(MediaTypeNames.Application.Json)]
public class TeachersProfilesController(ITeacherProfileCommandService teacherProfileCommandService,
    ITeacherQueryService teacherQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTeacherProfile([FromBody] CreateTeacherProfileResource resource)
    {
        var createProfileCommand = CreateTeacherProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var teacherProfile = await teacherProfileCommandService.Handle(createProfileCommand);
        if (teacherProfile is null) return BadRequest();
        var teacherProfileResource = TeacherProfileResourceFromEntityAssembler.ToResourceFromEntity(teacherProfile);
        return Ok(teacherProfileResource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTeacherProfiles()
    {
        var teacherProfiles = await teacherQueryService.Handle(new GetAllTeachersProfileQuery());
        var teacherResources 
            = teacherProfiles.Select(TeacherProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(teacherResources);
    }

    [HttpGet("{teacherId:int}")]
    public async Task<IActionResult> GetTeacherProfileById([FromRoute] int teacherId)
    {
        var teacherProfile = await teacherQueryService.Handle(new GetTeacherProfileByIdQuery(teacherId));
        if (teacherProfile is null) return NotFound();
        var teacherResource = TeacherProfileResourceFromEntityAssembler.ToResourceFromEntity(teacherProfile);
        return Ok(teacherResource);
    }
}