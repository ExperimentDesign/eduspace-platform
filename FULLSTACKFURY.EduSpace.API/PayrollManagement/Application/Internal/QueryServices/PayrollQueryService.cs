using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Services;

namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Application.Internal.QueryServices;

/// <summary>
/// Payroll query service
/// </summary>
/// <param name="payrollRepository">
/// The payroll repository for accessing payroll data.
/// </param>
public class PayrollQueryService(IPayrollRepository payrollRepository) : IPayrollQueryService
{
    /// <inheritdoc />
    public Task<IEnumerable<Payroll>> Handle(GetAllPayrollsQuery query)
    {
        return payrollRepository.ListAsync();
    }

    /// <inheritdoc />
    public Task<Payroll?> Handle(GetPayrollByIdQuery query)
    {
        return payrollRepository.FindByIdAsync(query.PayrollId);
    }

    /// <inheritdoc />
    public Task<IEnumerable<Payroll>> Handle(GetPayrollByTeacherIdQuery query)
    {
        return payrollRepository.FindAllByTeacherIdAsync(query.TeacherId);
    }
}