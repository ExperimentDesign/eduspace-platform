using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Aggregates;

namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Services;

public interface IPayrollCommandService
{
    Task<Payroll> Handle(CreatePayrollCommand command); // Devuelve Payroll en lugar de void
}