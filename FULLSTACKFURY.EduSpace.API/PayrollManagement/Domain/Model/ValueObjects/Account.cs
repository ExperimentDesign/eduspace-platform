namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.ValueObjects;

public record Account
{
    public string Value { get; }

    public Account(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) 
            throw new ArgumentException("The account cannot be null or empty.");
        Value = value;
    }

    public static implicit operator string(Account account) => account.Value;
}
