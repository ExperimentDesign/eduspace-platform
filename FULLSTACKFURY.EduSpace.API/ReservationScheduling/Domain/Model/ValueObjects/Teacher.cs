namespace FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.ValueObjects;

public class Teacher
{
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public Teacher(int id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
}