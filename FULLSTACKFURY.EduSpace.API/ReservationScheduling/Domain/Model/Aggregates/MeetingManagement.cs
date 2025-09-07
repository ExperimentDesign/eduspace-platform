using System.ComponentModel.DataAnnotations.Schema;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Entities;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.ValueObjects;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Domain.Model.ValueObjects;
using TeacherId = FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.ValueObjects.TeacherId;

namespace FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Aggregates;

public partial class Meeting
{
    public ICollection<MeetingSession> MeetingParticipants { get; } = new List<MeetingSession>();

    [NotMapped]
    public TeacherId TeacherId { get; set; }

public void AddTeacherToMeeting(int meetingId)
    {
        MeetingParticipants.Add(new MeetingSession(meetingId, TeacherId.TeacherIdentifier));
    }

public void TeacherIdBuilder(int teacherId)
    {
        TeacherId = new TeacherId(teacherId);
    }
    
    
}