using LowCortisol.Domain.Contexts.IdentityAccess.Events;
using LowCortisol.Domain.Contexts.IdentityAccess.ValueObjects;

namespace LowCortisol.Domain.Contexts.IdentityAccess.Aggregates;

public class UserAccount
{
    private readonly List<object> _domainEvents = new();

    public Guid Id { get; private set; }
    public PersonName Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public PhoneNumber Phone { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public UserRole Role { get; private set; }
    public bool IsActive { get; private set; }

    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    private UserAccount()
    {
        Id = Guid.Empty;
        Name = null!;
        Email = null!;
        Phone = null!;
        PasswordHash = null!;
        Role = null!;
    }

    public UserAccount(
        Guid id,
        PersonName name,
        EmailAddress email,
        PhoneNumber phone,
        PasswordHash passwordHash,
        UserRole role)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Name = name;
        Email = email;
        Phone = phone;
        PasswordHash = passwordHash;
        Role = role;
        IsActive = true;

        AddDomainEvent(new UserAccountRegisteredDomainEvent(Id));
    }

    public void ChangeName(PersonName name) => Name = name;
    public void ChangePhone(PhoneNumber phone) => Phone = phone;
    public void ChangeRole(UserRole role) => Role = role;

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        AddDomainEvent(new UserAccountDeactivatedDomainEvent(Id));
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        AddDomainEvent(new UserAccountActivatedDomainEvent(Id));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    private void AddDomainEvent(object domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}