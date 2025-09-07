namespace FULLSTACKFURY.EduSpace.API.IAM.Interfaces.REST.Resources;

public record AuthenticatedAccountResource(int Id, string Username, string Role, string Token);