namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Commands;

/// <summary>
/// Command to create a payroll entry.
/// </summary>
/// <param name="TeacherId">
/// The unique identifier for the teacher associated with this payroll.
/// </param>
/// <param name="SalaryAmount">
/// The base salary amount for the payroll entry.
/// </param>
/// <param name="SalaryBonus">
/// The bonus amount added to the base salary.
/// </param>
/// <param name="DatePayment">
/// The date when the payroll is issued.
/// </param>
public record CreatePayrollCommand(
    int TeacherId,
    decimal SalaryAmount,
    decimal SalaryBonus)
{};