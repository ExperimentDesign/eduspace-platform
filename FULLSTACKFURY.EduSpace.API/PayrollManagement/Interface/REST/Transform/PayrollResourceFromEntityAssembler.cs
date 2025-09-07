using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.ValueObjects;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Interface.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Interface.REST.Transform;

/// <summary>
/// Assembler class to transform Payroll to PayrollResource
/// </summary>
public static class PayrollResourceFromEntityAssembler
{
    /// <summary>
    /// Transforma una entidad Payroll en un PayrollResource.
    /// </summary>
    /// <param name="entity">
    /// La entidad <see cref="Payroll"/> a transformar.
    /// </param>
    /// <returns>
    /// El <see cref="PayrollResource"/> resultante con los valores de la entidad.
    /// </returns>
    ///
 
    public static PayrollResource ToResourceFromEntity(Payroll entity)
    {
        return new PayrollResource(
            entity.TeacherId,
            entity.SalaryAmount.Value, // Valor directo de SalaryAmount
            entity.PayrollAdjustment.PensionContribution, // Valor directo de PensionContribution
            entity.PayrollAdjustment.SalaryBonus // Valor directo de SalaryBonus desde PayrollAdjustment
        );
    }

}
