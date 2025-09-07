using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Repositories;

/// <summary>
/// Represents the payroll repository in the Payroll Management system.
/// </summary>
public interface IPayrollRepository : IBaseRepository<Payroll>
{
    /// <summary>
    /// Retrieves a payroll entry by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the payroll entry.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the payroll entry if found; otherwise, null.
    /// </returns>
    Task<Payroll?> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all payroll entries associated with a specific teacher ID.
    /// </summary>
    /// <param name="teacherId">The unique identifier of the teacher.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a list of payroll entries for the specified teacher.
    /// </returns>
    Task<IEnumerable<Payroll>> FindAllByTeacherIdAsync(int teacherId);
}