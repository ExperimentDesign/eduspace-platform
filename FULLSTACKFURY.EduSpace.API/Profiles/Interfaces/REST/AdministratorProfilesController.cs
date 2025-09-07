using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Services;
using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.REST.Resources;
using FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Interfaces.REST;

[ApiController]
[Route("api/v1/administrator-profiles")]
[Produces(MediaTypeNames.Application.Json)]
public class AdministratorProfilesController(IAdminProfileCommandService profileCommandService, 
    IAdminProfileQueryService profileQueryService)
: ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAdministratorProfile([FromBody] CreateAdminProfileResource resource)
    {
        var createAdminProfileCommand = CreateAdminProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        var adminProfile = await profileCommandService.Handle(createAdminProfileCommand);
        
        if (adminProfile is null) return BadRequest();

        var adminProfileResource = AdminProfileResourceFromEntityAssembler.ToResourceFromEntity(adminProfile);
        
        return Ok(adminProfileResource); 
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAdministratorProfiles()
    {
        var administratorProfiles = await profileQueryService.Handle(new GetAllAdministratorsProfileQuery());
        var administratorResources =
            administratorProfiles.Select(AdminProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(administratorResources);
    }
    
    [HttpGet("{administratorId:int}")]
    public async Task<IActionResult> GetTeacherProfileById([FromRoute] int administratorId)
    {
        var administratorProfile = await profileQueryService.Handle(new GetAdministratorProfileByIdQuery(administratorId));
        if (administratorProfile is null) return NotFound();
        var administratorResource = AdminProfileResourceFromEntityAssembler.ToResourceFromEntity(administratorProfile);
        return Ok(administratorResource);
    }
}