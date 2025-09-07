using System.Text.Json.Serialization;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.ValueObjects;

namespace FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Aggregates;

public class Account
{

    public int Id { get; }
    public string Username { get; private set; }
    [JsonIgnore] public string PasswordHash { get; private set; }
    public ERoles Role { get; private set; }

    public Account UpdateUsername(string username)
    {
        Username = username;
        return this;
    }

    public Account UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
    
    public Account(string username, string passwordHash, string role)
    {
        Username = username;
        PasswordHash = passwordHash;
        Role = Enum.Parse<ERoles>(role);
    }
    
    public string GetRole() => Role.ToString();

    public Account() { }
}