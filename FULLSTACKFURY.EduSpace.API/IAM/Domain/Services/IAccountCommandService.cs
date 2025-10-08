using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Commands;

namespace FULLSTACKFURY.EduSpace.API.IAM.Domain.Services;

public interface IAccountCommandService
{
    Task Handle(SignUpCommand command);
    Task Handle(SignInCommand command);
    Task<(Account account, string token, int? profileId)> Handle(VerifyCodeCommand command);
}