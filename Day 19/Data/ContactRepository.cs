using PhoneBook.Models;

namespace PhoneBook.Data;

/// <summary>
/// Простое in-memory хранилище контактов (без базы данных).
/// Зарегистрировано как Singleton — данные живут пока работает приложение.
/// </summary>
public class ContactRepository
{
    private readonly List<Contact> _contacts = new()
    {
        new Contact { Id = 1, Name = "Алексей Петров",   PhoneNumber = "+7 (999) 123-45-67", Email = "aleksey@example.com" },
        new Contact { Id = 2, Name = "Мария Иванова",    PhoneNumber = "+7 (999) 234-56-78", Email = "maria@example.com"   },
        new Contact { Id = 3, Name = "Дмитрий Сидоров",  PhoneNumber = "+7 (999) 345-67-89", Email = null                  },
        new Contact { Id = 4, Name = "Елена Козлова",    PhoneNumber = "+7 (999) 456-78-90", Email = "elena@example.com"   },
        new Contact { Id = 5, Name = "Андрей Новиков",   PhoneNumber = "+7 (999) 567-89-01", Email = "andrey@example.com"  },
    };

    private int _nextId = 6;

    public IEnumerable<Contact> GetAll() => _contacts.AsReadOnly();

    public IEnumerable<Contact> Search(string name) =>
        _contacts.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

    public Contact? GetById(int id) => _contacts.FirstOrDefault(c => c.Id == id);

    public void Add(Contact contact)
    {
        contact.Id = _nextId++;
        _contacts.Add(contact);
    }

    public bool Delete(int id)
    {
        var contact = GetById(id);
        if (contact is null) return false;
        _contacts.Remove(contact);
        return true;
    }
}
