using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.IAM.Interfaces.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.IAM.Interfaces.REST.Transform;

public static class AuthenticatedAccountResourceFromEntityAssembler
{
    public static AuthenticatedAccountResource ToResourceFromEntity(Account entity, string token)
    {
        return new AuthenticatedAccountResource(entity.Id, entity.Username, entity.GetRole(), token);
    }
}