using Google.Protobuf.WellKnownTypes;

namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.ValueObjects;


public record PayrollAdjustment
{
    public decimal PensionContribution { get; init; }
    public decimal SalaryBonus { get; init; } 

    public PayrollAdjustment(decimal pensionContribution, decimal salaryBonus)
    {
        if (pensionContribution < 0) throw new ArgumentException("The pension contribution cannot be negative.");
        if (salaryBonus < 0) throw new ArgumentException("The salary bonus cannot be negative.");
        
        PensionContribution = pensionContribution;
        SalaryBonus = salaryBonus;
    }
};