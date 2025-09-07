using FULLSTACKFURY.EduSpace.API.BreakdownManagement.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.BreakdownManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.BreakdownManagement.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.BreakdownManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.BreakdownManagement.Application.Internal.CommandServices
{
    public class ReportCommandService : IReportCommandService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReportCommandService(IReportRepository reportRepository, IUnitOfWork unitOfWork)
        {
            _reportRepository = reportRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Report?> Handle(CreateReportCommand command)
        {
            var report = new Report(command);
            try
            {
                await _reportRepository.AddAsync(report);
                await _unitOfWork.CompleteAsync();
                return report;
            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred while creating the report: {e.Message}");
            }
        }

        // Puedes agregar más métodos según sea necesario para actualizar el estado, etc.
    }
}