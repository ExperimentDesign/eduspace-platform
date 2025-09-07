namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.ValueObjects;

public record SalaryAmount
{
    public decimal Value { get; }

    public SalaryAmount(decimal value)
    {
        if (value < 0) throw new ArgumentException("The salary amount cannot be negative.");
        Value = value;
    }

    public static implicit operator decimal(SalaryAmount amount) => amount.Value;
}
