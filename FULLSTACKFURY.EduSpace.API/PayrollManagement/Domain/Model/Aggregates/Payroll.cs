using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.ValueObjects;

namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Aggregates;

/// <summary>
/// Payroll aggregate root entity.
/// </summary>
/// <remarks>
/// This class is used to represent payroll details for a teacher in the application.
/// </remarks>
///

public partial class Payroll
{
    public int Id { get; private set; }
    public int TeacherId { get; private set; }
    public SalaryAmount SalaryAmount { get; private set; }
    public PayrollAdjustment PayrollAdjustment { get; private set; }
    public decimal SalaryNet { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Payroll"/> class based on the specified command.
    /// </summary>
    /// <param name="command">
    /// The <see cref="CreatePayrollCommand"/> containing payroll details to initialize this instance.
    /// </param>
   
   public Payroll() { }
    public Payroll(CreatePayrollCommand command)
    {
        TeacherId = command.TeacherId;
        SalaryAmount = new SalaryAmount(command.SalaryAmount);
        PayrollAdjustment = new PayrollAdjustment(CalculatePensionContribution(command.SalaryAmount), command.SalaryBonus);
        SalaryNet = CalculateSalaryNet(command.SalaryAmount,PayrollAdjustment.PensionContribution, command.SalaryBonus);
    }
}
