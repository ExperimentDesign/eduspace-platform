using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.ValueObjects;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Domain.Services;

public interface ITeacherQueryService
{
    Task<IEnumerable<TeacherProfile>> Handle(GetAllTeachersProfileQuery query);
    Task<TeacherProfile?> Handle(GetTeacherProfileByIdQuery query);
}