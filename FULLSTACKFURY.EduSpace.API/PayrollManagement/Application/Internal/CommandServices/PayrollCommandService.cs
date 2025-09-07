using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Application.Internal.CommandServices;

/// <summary>
/// Represents a command service for managing payroll entities.
/// </summary>
/// <param name="repository">
/// The repository for payroll entities.
/// </param>
/// <param name="unitOfWork">
/// The unit of work for managing transactions.
/// </param>
public class PayrollCommandService : IPayrollCommandService
{
    private readonly IPayrollRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="PayrollCommandService"/> class.
    /// </summary>
    /// <param name="repository">The payroll repository instance.</param>
    /// <param name="unitOfWork">The unit of work for transaction management.</param>
    public PayrollCommandService(IPayrollRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the creation of a new payroll entry.
    /// </summary>
    /// <param name="command">The <see cref="CreatePayrollCommand"/> containing payroll details.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the created payroll entry.
    /// </returns>
    public async Task<Payroll> Handle(CreatePayrollCommand command)
    {
        var payroll = new Payroll(command);
        await _repository.AddAsync(payroll);
        await _unitOfWork.CompleteAsync();
        return payroll;
    }

}
