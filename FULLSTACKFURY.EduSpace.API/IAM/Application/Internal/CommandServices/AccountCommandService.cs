using FULLSTACKFURY.EduSpace.API.IAM.Application.Internal.OutboundServices;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Repository;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Services;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.IAM.Application.Internal.CommandServices;

public class AccountCommandService (IUnitOfWork unitOfWork, IAccountRepository accountRepository
    , ITokenService tokenService, IHashingService hashingService)
:IAccountCommandService
{
    public async Task Handle(SignUpCommand command)
    {
        if (accountRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} is already taken");
        
        var hashedPassword = hashingService.HashPassword(command.Password);
        var account = new Account(command.Username, hashedPassword, command.Role);

        try
        {
            await accountRepository.AddAsync(account);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occured while creating the account: {e.Message}");
        }
    }

    //TODO: This should return a token
    public async Task<(Account account, string token)> Handle(SignInCommand command)
    {
        Console.WriteLine("AAAAAAAAAAAAAA");
        Console.WriteLine(accountRepository.ExistsByUsername(command.Username));
        var account = await accountRepository.FindByUsername(command.Username);
        if (account is null) throw new Exception("Invalid username or password");
        if(!hashingService.VerifyPassword(command.Password, account.PasswordHash))
            throw new Exception("Invalid username or password");
        var token = tokenService.GenerateToken(account);

        return (account, token);
    }
}