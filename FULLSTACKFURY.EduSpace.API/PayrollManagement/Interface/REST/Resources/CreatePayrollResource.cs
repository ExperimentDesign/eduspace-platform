namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Interface.REST.Resources;

/// <summary>
/// Represents a resource to create a payroll entry.
/// </summary>
/// <param name="TeacherId">
/// The unique identifier for the teacher associated with this payroll.
/// </param>
/// <param name="SalaryAmount">
/// The base salary amount for the payroll entry.
/// </param>
/// <param name="PensionContribution">
/// The amount contributed to the pension fund.
/// </param>
/// <param name="SalaryBonus">
/// The bonus amount added to the base salary.
/// </param>
/// <param name="DatePayment">
/// The date when the payroll is issued.
/// </param>

public record CreatePayrollResource(decimal SalaryAmount, decimal PensionContribution, decimal SalaryBonus);