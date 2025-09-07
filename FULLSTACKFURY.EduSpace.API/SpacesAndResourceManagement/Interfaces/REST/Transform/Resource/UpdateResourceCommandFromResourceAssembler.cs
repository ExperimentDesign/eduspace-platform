using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Domain.Model.Commands.Resource;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Interfaces.REST.Resources.Resource;

namespace FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Interfaces.REST.Transform.Resource;

public static class UpdateResourceCommandFromResourceAssembler
{
    public static UpdateResourceCommand ToCommandFromResource(int Id, UpdateResourceResource resource)
    {
        return new UpdateResourceCommand(
            Id,
            resource.Name,
            resource.KindOfResource,
            resource.ClassroomId
        );
    }
}