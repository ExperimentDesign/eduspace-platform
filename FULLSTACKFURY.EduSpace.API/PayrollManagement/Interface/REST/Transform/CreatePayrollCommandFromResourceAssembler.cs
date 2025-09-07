using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Interface.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Interface.REST.Transform;

/// <summary>
/// Assembler class to transform CreatePayrollResource to CreatePayrollCommand
/// </summary>
public static class CreatePayrollCommandFromResourceAssembler
{
    /// <summary>
    /// Transform CreatePayrollResource to CreatePayrollCommand
    /// </summary>
    /// <param name="resource">
    /// The <see cref="CreatePayrollResource"/> resource to transform
    /// </param>
    /// <returns>
    /// The resulting <see cref="CreatePayrollCommand"/> command with the values from the resource
    /// </returns>
    public static CreatePayrollCommand ToCommand(int teacherId, CreatePayrollResource resource)
    {
        return new CreatePayrollCommand(teacherId, resource.SalaryAmount, resource.SalaryBonus);
    }
}