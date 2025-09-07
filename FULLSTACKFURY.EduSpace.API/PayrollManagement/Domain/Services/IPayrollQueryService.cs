using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Queries;

namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Services;

/// <summary>
/// Represents the payroll query service in the Payroll Management system.
/// </summary>
public interface IPayrollQueryService
{
    /// <summary>
    /// Handles the query to retrieve all payroll entries.
    /// </summary>
    /// <param name="query">The <see cref="GetAllPayrollsQuery"/> to handle.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of all payroll entries.
    /// </returns>
    Task<IEnumerable<Payroll>> Handle(GetAllPayrollsQuery query);

    /// <summary>
    /// Handles the query to retrieve a payroll entry by its unique identifier.
    /// </summary>
    /// <param name="query">The <see cref="GetPayrollByIdQuery"/> containing the payroll ID.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the payroll entry if found; otherwise, null.
    /// </returns>
    Task<Payroll?> Handle(GetPayrollByIdQuery query);

    /// <summary>
    /// Handles the query to retrieve all payroll entries for a specific teacher.
    /// </summary>
    /// <param name="query">The <see cref="GetPayrollByTeacherIdQuery"/> containing the teacher ID.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of payroll entries associated with the specified teacher.
    /// </returns>
    Task<IEnumerable<Payroll>> Handle(GetPayrollByTeacherIdQuery query);
}