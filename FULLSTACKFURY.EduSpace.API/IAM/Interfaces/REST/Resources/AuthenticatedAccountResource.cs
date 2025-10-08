namespace FULLSTACKFURY.EduSpace.API.IAM.Interfaces.REST.Resources;

public record AuthenticatedAccountResource(int Id, int ProfileId, string Username, string Role, string Token);