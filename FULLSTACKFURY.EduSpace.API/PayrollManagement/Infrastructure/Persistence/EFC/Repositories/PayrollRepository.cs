using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.EFC.Repositories;

using Microsoft.EntityFrameworkCore;

namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Infrastructure.Persistence.EFC.Repositories;

public class PayrollRepository : BaseRepository<Payroll>, IPayrollRepository
{
    public PayrollRepository(AppDbContext context) : base(context) { }

    /// <summary>
    /// Retrieves all payrolls from the database.
    /// </summary>
    public async Task<IEnumerable<Payroll>> GetAllAsync()
    {
        return await Context.Set<Payroll>().ToListAsync();
    }

    /// <summary>
    /// Retrieves a payroll by its ID.
    /// </summary>
    public async Task<Payroll?> GetByIdAsync(int id)
    {
        return await Context.Set<Payroll>().FindAsync(id);
    }

    /// <summary>
    /// Adds a new payroll to the database.
    /// </summary>
    public async Task AddAsync(Payroll payroll)
    {
        await Context.Set<Payroll>().AddAsync(payroll);
    }

    /// <summary>
    /// Updates an existing payroll in the database.
    /// </summary>
    public void Update(Payroll payroll)
    {
        Context.Set<Payroll>().Update(payroll);
    }

    /// <summary>
    /// Deletes a payroll from the database.
    /// </summary>
    public void Delete(Payroll payroll)
    {
        Context.Set<Payroll>().Remove(payroll);
    }

    /// <summary>
    /// Finds all payrolls associated with a specific teacher ID.
    /// </summary>
    public async Task<IEnumerable<Payroll>> FindAllByTeacherIdAsync(int teacherId)
    {
        return await Context.Set<Payroll>()
            .Where(p => p.TeacherId == teacherId)
            .ToListAsync();
    }
}