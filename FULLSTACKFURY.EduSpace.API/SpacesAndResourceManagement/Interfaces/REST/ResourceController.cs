using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Domain.Model.Commands.Resource;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Interfaces.REST.Resources.Resource;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Interfaces.REST.Transform.Resource;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Interfaces.REST;

[ApiController]
[Route("api/v1")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Resource Endpoints")]
public class ResourceController(IResourceQueryService resourceQueryService, IResourceCommandService resourceCommandService): ControllerBase
{
    
    /// <summary>
    /// Get resource by id
    /// </summary>
    /// <param name="id">
    /// The resource  id to get
    /// </param>
    /// <returns>
    /// The <see cref="ResourceResource"/> resource if found, otherwise returns <see cref="NotFoundResult"/>
    /// </returns>
    [HttpGet("classrooms/resources/{resourceId:int}")]
    [SwaggerOperation(
        Summary = "Get a resource by its ID",
        Description = "Get a resource by its ID",
        OperationId = "GetResourceById"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The resource was successfully retrieved", typeof(ResourceResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The resource was not found")]
    public async Task<IActionResult> GetResourceById([FromRoute] int id)
    {
        var getResourceByIdQuery = new GetResourceByIdQuery(id);
        var resource = await resourceQueryService.Handle(getResourceByIdQuery);
        if (resource is null) return NotFound();
        var resourceResource = ResourceResourceFromEntityAssembler.ToResourceFromEntity(resource); 
        return Ok(resourceResource);
    }

    /// <summary>
    /// Create a resource
    /// </summary>
    /// <param name="createResource">
    /// The <see cref="CreateResourceResource"/> to create the resource from
    /// </param>
    /// <returns>
    /// The <see cref="ResourceResource"/> resource if created, otherwise returns <see cref="BadRequestResult"/>
    /// </returns>
    [HttpPost("classrooms/{classroomId:int}/resources")]
    [SwaggerOperation(
        Summary = "Create a new resource",  
        Description = "Create a new resource",
        OperationId = "CreateResource"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "The resource was successfully created", typeof(ResourceResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The resource was not created")]
    public async Task<IActionResult> CreateResource([FromRoute] int classroomId , [FromBody] CreateResourceResource createResource)
    {
        var createResourceCommand = CreateResourceCommandFromResourceAssembler.ToCommandFromResource(classroomId, createResource);
        var resource = await resourceCommandService.Handle(createResourceCommand);
        if (resource is null) return BadRequest();
        var resourceResource = ResourceResourceFromEntityAssembler.ToResourceFromEntity(resource);
        return CreatedAtAction(nameof(GetResourceById), new { resourceId = resource.Id }, resourceResource);
    }
    
    /// <summary>
    /// Get all resources 
    /// </summary>
    /// <returns>
    /// The list of <see cref="ResourceResource"/> resources
    /// </returns>
    [HttpGet("classrooms/resources")]
    [SwaggerOperation(
        Summary = "Get all resources",
        Description = "Get all resources",
        OperationId = "GetAllResources"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The resources were successfully retrieved", typeof(IEnumerable<ResourceResource>))]
    public async Task<IActionResult> GetAllResources()
    {
        var getAllResourcesQuery = new GetAllResourcesQuery();
        var resources = await resourceQueryService.Handle(getAllResourcesQuery);
        var resourceResources = resources.Select(ResourceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resourceResources);
    }
    
        [HttpPut("classrooms/resources/{id:int}")]
    [SwaggerOperation(
        Summary = "Updates a resource",
        Description = "Updates a resource by its ID with the provided details",
        OperationId = "UpdateResource"
    )]
    [SwaggerResponse(200, "The resource was updated successfully", typeof(ResourceResource))]
    [SwaggerResponse(404, "The resource was not found")]
    public async Task<IActionResult> UpdateResource([FromRoute] int id, [FromBody] UpdateResourceResource resource)
    {
        try
        {
            // Map the resource to the UpdateMeetingCommand
            var updateResourceCommand = UpdateResourceCommandFromResourceAssembler.ToCommandFromResource(id, resource);

            // Handle the update command
            var updatedResource = await resourceCommandService.Handle(updateResourceCommand);

            // If the meeting was not updated, return not found
            if (updatedResource is null)
                return NotFound(new { Message = "Meeting not found." });

            // Map the updated meeting entity to the resource
            var resourceResource = ResourceResourceFromEntityAssembler.ToResourceFromEntity(updatedResource);
            return Ok(resourceResource);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
    
    [HttpDelete("classrooms/resources/{id:int}")]
    [SwaggerOperation(
        Summary = "Deletes a resource",
        Description = "Deletes the resource specified by its ID",
        OperationId = "DeleteResource"
    )]
    [SwaggerResponse(200, "The resource was deleted successfully.")]
    [SwaggerResponse(404, "Meeting not found.")]
    public async Task<IActionResult> DeleteResource([FromRoute] int id)
    {
        try
        {
            var deleteResourceCommand = new DeleteResourceCommand(id);
            await resourceCommandService.Handle(deleteResourceCommand);
            return Ok($"Resource with ID {id} was deleted successfully.");
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        }
    }
}