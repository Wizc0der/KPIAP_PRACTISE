using PhoneBook.Data;
using PhoneBook.Models;
using PhoneBook.ViewModels;

namespace PhoneBook.Services;

/// <summary>
/// Реализация <see cref="IContactService"/>.
/// Инкапсулирует бизнес-логику и маппинг ViewModel → Model,
/// делегируя хранение данных в <see cref="ContactRepository"/>.
/// </summary>
public class ContactService : IContactService
{
    private readonly ContactRepository _repo;

    // ContactRepository внедряется через DI
    public ContactService(ContactRepository repo)
    {
        _repo = repo;
    }

    /// <inheritdoc/>
    public IEnumerable<Contact> GetAll() => _repo.GetAll();

    /// <inheritdoc/>
    public IEnumerable<Contact> Search(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return _repo.GetAll();

        return _repo.Search(name.Trim());
    }

    /// <inheritdoc/>
    public Contact Add(ContactViewModel vm)
    {
        // Маппинг: ContactViewModel  →  Contact (доменная модель)
        var contact = new Contact
        {
            Name        = vm.FullName,
            PhoneNumber = vm.PhoneNumber.Trim(),
            Email       = vm.Email.Trim()
        };

        _repo.Add(contact);
        return contact;
    }

    /// <inheritdoc/>
    public bool Delete(int id) => _repo.Delete(id);
}
