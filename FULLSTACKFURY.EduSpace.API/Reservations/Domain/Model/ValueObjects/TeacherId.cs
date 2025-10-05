namespace FULLSTACKFURY.EduSpace.API.EventsScheduling.Domain.Model.ValueObjects;

public record TeacherId
{
    public int TeacherIdentifier { get; init; }

    public TeacherId(int TeacherIdentifier)
    {
        if(TeacherIdentifier<=0) throw new ArgumentException("Teacher Id cannot be less than or equal to 0");
        this.TeacherIdentifier = TeacherIdentifier;
    }
    TeacherId() {}
}