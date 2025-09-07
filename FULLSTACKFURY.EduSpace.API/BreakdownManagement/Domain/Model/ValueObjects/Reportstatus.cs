namespace FULLSTACKFURY.EduSpace.API.BreakdownManagement.Domain.Model.ValueObjects;

public record ReportStatus
{
    public string Value { get; init; }

    private ReportStatus(string value)
    {
        Value = value;
    }

    public static ReportStatus EnProceso => new ReportStatus("en proceso");
    public static ReportStatus Completado => new ReportStatus("completado");
    public static ReportStatus Cancelado => new ReportStatus("cancelado");
}