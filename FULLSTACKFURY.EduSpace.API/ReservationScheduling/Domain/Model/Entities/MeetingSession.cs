using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.ValueObjects;

namespace FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Entities;

public class MeetingSession
{
    public int MeetingId { get; set; }
    
    public int TeacherId { get; set; }
    
    public Meeting Meeting { get; set; }
    public TeacherProfile Teacher { get; set; }

    public MeetingSession()
    {
        Meeting = default!;
        Teacher = default!;
    }

    public MeetingSession(int teacherId, int meetingId)
    {
        TeacherId = teacherId;
        MeetingId = meetingId;
        Meeting = default!;
        Teacher = default!;
    }
}