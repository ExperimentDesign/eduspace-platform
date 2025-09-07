using FULLSTACKFURY.EduSpace.API.BreakdownManagement.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.BreakdownManagement.Domain.Model.ValueObjects;

namespace FULLSTACKFURY.EduSpace.API.BreakdownManagement.Domain.Model.Aggregates;

public record Report
{
    public int Id { get; init; }
    public string KindOfReport { get; init; }
    public string Description { get; init; }
    public ResourceId ResourceId { get; init; }
    public DateTime CreatedAt { get; init; }
    public ReportStatus Status { get; init; }

    public Report(string kindOfReport, string description, int resourceId, DateTime createdAt, ReportStatus status = null)
    {
        KindOfReport = kindOfReport;
        Description = description;
        ResourceId = new ResourceId(resourceId);
        CreatedAt = createdAt;
        Status = status ?? ReportStatus.EnProceso; // Default status
    }

    public Report(CreateReportCommand command)
    {
        KindOfReport = command.KindOfReport;
        Description = command.Description;
        ResourceId = new ResourceId(command.ResourceId);
        CreatedAt = command.CreatedAt;
        Status = ReportStatus.EnProceso; // Default status
    }
}