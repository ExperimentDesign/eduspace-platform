using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Interfaces.REST.Resources.Resource;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Interfaces.REST.Transform.Resource;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Interfaces.REST;

/// <summary>
/// Get all resources by classroom id
/// </summary>
/// <param name="id">
/// The classroom id to get resources for
/// </param>
/// <returns>
/// The resources  for the classroom
/// </returns>
[ApiController]
[Route("api/v1/classrooms/{id:int}/resources")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Classrooms")]
public class ClassroomResourcesController(IResourceQueryService resourceQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all resources for a classroom",
        Description = "Get all resources for a classroom",
        OperationId = "Get All ResourcesForClassroom"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Resources for classroom", typeof(IEnumerable<ResourceResource>))]
    public async Task<IActionResult> GetAllResourceByClassroomId(int id)
    {
        var getAllResourcesByClassroomIdQuery = new GetAllResourcesByClassroomIdQuery(id);
        var resources = await resourceQueryService.Handle(getAllResourcesByClassroomIdQuery);
        var resourceResources = resources.Select(ResourceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resourceResources);
    }
}