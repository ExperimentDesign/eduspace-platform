namespace FULLSTACKFURY.EduSpace.API.EventsScheduling.Domain.Model.ValueObjects;

public record AreaId
{
    public int Identifier { get; init; }

    public AreaId(int teacherIdentifier)
    {
        if(teacherIdentifier <= 0) throw new ArgumentException("Area Id cannot be less than or equal to 0");
        Identifier = teacherIdentifier;
    }
    public AreaId() {}
}