using FULLSTACKFURY.EduSpace.API.IAM.Application.Internal.OutboundServices;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Repository;
using FULLSTACKFURY.EduSpace.API.IAM.Domain.Services;
using FULLSTACKFURY.EduSpace.API.Profiles.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FULLSTACKFURY.EduSpace.API.IAM.Application.Internal.CommandServices;

public class AccountCommandService (IUnitOfWork unitOfWork, IAccountRepository accountRepository, 
    ITokenService tokenService, IHashingService hashingService, IEmailService emailService,
    ITeacherProfileRepository teacherProfileRepository, IAdminProfileRepository adminProfileRepository)
    : IAccountCommandService
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
    
    public async Task Handle(SignInCommand command)
    {
        var account = await accountRepository.FindByUsername(command.Username);
        if (account is null || !hashingService.VerifyPassword(command.Password, account.PasswordHash))
        {
            throw new Exception("Invalid username or password");
        }

        var random = new Random();
        var code = random.Next(100000, 999999).ToString();

        var verificationCode = new VerificationCode
        {
            AccountId = account.Id,
            Code = code,
            ExpirationDate = DateTime.UtcNow.AddMinutes(10),
            IsUsed = false
        };
        
        if (unitOfWork is UnitOfWork dbContextUnitOfWork)
        {
            await dbContextUnitOfWork._context.Set<VerificationCode>().AddAsync(verificationCode);
            await unitOfWork.CompleteAsync();
        }
        else
        {
            throw new InvalidOperationException("The configured unit of work is not a supported type.");
        }

        string? userEmail = null;
        var teacherProfiles = await teacherProfileRepository.ListAsync();
        var teacher = teacherProfiles.FirstOrDefault(t => t.AccountId.Id == account.Id);
        
        if (teacher != null)
        {
            userEmail = teacher.ProfilePrivateInformation.ObtainEmail;
        }
        else
        {
             var adminProfiles = await adminProfileRepository.ListAsync();
             var admin = adminProfiles.FirstOrDefault(a => a.AccountId.Id == account.Id);
             if (admin != null)
             {
                 userEmail = admin.ProfilePrivateInformation.ObtainEmail;
             }
        }
        
        if (string.IsNullOrEmpty(userEmail))
        {
            throw new Exception("User email not found.");
        }

        await emailService.SendEmailAsync(userEmail, "Tu código de verificación de EduSpace", $"Tu código es: {code}");
    }

    public async Task<(Account account, string token, int? profileId)> Handle(VerifyCodeCommand command)
    {
        var account = await accountRepository.FindByUsername(command.Username);
        if (account is null) throw new Exception("Invalid username.");

        if (unitOfWork is not UnitOfWork dbContextUnitOfWork)  throw new Exception("Database context not available");
        
        var verificationCode = await dbContextUnitOfWork._context.Set<VerificationCode>()
            .FirstOrDefaultAsync(vc => vc.AccountId == account.Id && vc.Code == command.Code && !vc.IsUsed && vc.ExpirationDate > DateTime.UtcNow);

        if (verificationCode is null)
        {
            throw new Exception("Invalid or expired verification code.");
        }

        verificationCode.IsUsed = true;
        await unitOfWork.CompleteAsync();

        var token = tokenService.GenerateToken(account);
        
        int? profileId = null;
        if (account.GetRole() == "RoleTeacher")
        {
            var teacherProfile = await teacherProfileRepository.ListAsync().ContinueWith(t => t.Result.FirstOrDefault(p => p.AccountId.Id == account.Id));
            profileId = teacherProfile?.Id;
        }
        else if (account.GetRole() == "RoleAdmin")
        {
            var adminProfile = await adminProfileRepository.ListAsync().ContinueWith(t => t.Result.FirstOrDefault(p => p.AccountId.Id == account.Id));
            profileId = adminProfile?.Id;
        }
        
        return (account, token, profileId);
    }
}