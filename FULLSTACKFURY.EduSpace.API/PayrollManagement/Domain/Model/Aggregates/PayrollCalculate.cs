namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Aggregates;

public partial class Payroll
{
    private decimal CalculateSalaryNet(decimal salaryAmount, decimal pensionContribution, decimal salaryBonus)
    {
        return salaryAmount - pensionContribution + salaryBonus;
    }

    private decimal CalculatePensionContribution(decimal decimalSalaryAmount)
    {
        return decimalSalaryAmount * 0.1m;
    }
}