using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Commands;

namespace FULLSTACKFURY.EduSpace.API.IAM.Domain.Services;

public interface IAccountCommandService
{
    Task Handle(SignUpCommand command);
    Task<(Account account, string token)> Handle(SignInCommand command);
}