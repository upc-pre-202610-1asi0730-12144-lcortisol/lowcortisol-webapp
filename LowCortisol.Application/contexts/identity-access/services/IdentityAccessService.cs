using LowCortisol.Application.Contexts.IdentityAccess.Commands;
using LowCortisol.Application.Contexts.IdentityAccess.DTOs;
using LowCortisol.Application.Security;
using LowCortisol.Domain.Contexts.IdentityAccess.Aggregates;
using LowCortisol.Domain.Contexts.IdentityAccess.Repositories;
using LowCortisol.Domain.Contexts.IdentityAccess.ValueObjects;
using LowCortisol.Application.Common.Interfaces;

namespace LowCortisol.Application.Contexts.IdentityAccess.Services;

public class IdentityAccessService : IIdentityAccessService
{
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public IdentityAccessService(
        IUserAccountRepository userAccountRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userAccountRepository = userAccountRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, string Error)> RegisterAsync(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var email = new EmailAddress(command.Email);

            var alreadyExists = await _userAccountRepository.ExistsByEmailAsync(email, cancellationToken);
            if (alreadyExists)
                return (false, "The email is already registered.");

            var passwordHash = _passwordHasher.Hash(command.Password);

            var userAccount = new UserAccount(
                Guid.NewGuid(),
                new PersonName(command.Name),
                email,
                new PhoneNumber(command.Phone),
                new PasswordHash(passwordHash),
                new UserRole(command.Role)
            );

            await _userAccountRepository.AddAsync(userAccount, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return (true, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool Success, string Error, UserAccountDto? User)> LoginAsync(
        LoginUserCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var email = new EmailAddress(command.Email);

            var userAccount = await _userAccountRepository.FindByEmailAsync(email, cancellationToken);
            if (userAccount is null)
                return (false, "Invalid credentials.", null);

            var isValid = _passwordHasher.Verify(command.Password, userAccount.PasswordHash.Value);
            if (!isValid)
                return (false, "Invalid credentials.", null);

            var dto = MapToDto(userAccount);
            return (true, string.Empty, dto);
        }
        catch (Exception ex)
        {
            return (false, ex.Message, null);
        }
    }

    public async Task<UserAccountDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var userAccount = await _userAccountRepository.FindByIdAsync(id, cancellationToken);
        return userAccount is null ? null : MapToDto(userAccount);
    }

    private static UserAccountDto MapToDto(UserAccount userAccount)
    {
        return new UserAccountDto
        {
            Id = userAccount.Id,
            Name = userAccount.Name.Value,
            Email = userAccount.Email.Value,
            Phone = userAccount.Phone.Value,
            Role = userAccount.Role.Value,
            IsActive = userAccount.IsActive
        };
    }
}