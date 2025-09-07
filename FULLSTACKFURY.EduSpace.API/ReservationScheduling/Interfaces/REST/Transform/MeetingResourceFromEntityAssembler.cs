using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.ValueObjects;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Interfaces.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.ReservationScheduling.Interfaces.REST.Transform;

public class MeetingResourceFromEntityAssembler
{
    public static MeetingResource ToResourceFromEntity(Meeting entity)
    {
        
        return new MeetingResource(
            entity.Id,
            entity.Title,
            entity.Description,
            entity.Date,
            entity.StartTime,
            entity.EndTime,
            entity.AdministratorId,
            entity.ClassroomId);
    }
}