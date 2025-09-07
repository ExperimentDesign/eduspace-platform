using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.Commands;

namespace FULLSTACKFURY.EduSpace.API.Profiles.Domain.Services;

public interface ITeacherProfileCommandService
{
    Task<TeacherProfile?> Handle(CreateTeacherProfileCommand command);
}