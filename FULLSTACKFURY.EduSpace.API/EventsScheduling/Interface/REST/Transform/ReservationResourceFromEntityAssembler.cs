using FULLSTACKFURY.EduSpace.API.EventsScheduling.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.EventsScheduling.Interface.REST.Resources;

namespace FULLSTACKFURY.EduSpace.API.EventsScheduling.Interface.REST.Transform;

public static class ReservationResourceFromEntityAssembler {
    public static ReservationResource ToResourceFromEntity(Reservation entity)
    {
        return new ReservationResource(entity.Id, entity.ReservationDate.Start, entity.ReservationDate.End);
    }
    
}