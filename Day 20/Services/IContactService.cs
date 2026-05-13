using PhoneBook.Models;
using PhoneBook.ViewModels;

namespace PhoneBook.Services;

/// <summary>
/// Сервис для работы с контактами.
/// Контроллер зависит только от этого интерфейса — конкретная реализация
/// подменяется через DI, что упрощает тестирование.
/// </summary>
public interface IContactService
{
    /// <summary>Вернуть все контакты.</summary>
    IEnumerable<Contact> GetAll();

    /// <summary>Найти контакты по имени (регистронезависимо).</summary>
    IEnumerable<Contact> Search(string name);

    /// <summary>Добавить контакт из ViewModel.</summary>
    Contact Add(ContactViewModel viewModel);

    /// <summary>Удалить контакт по Id. Возвращает false, если не найден.</summary>
    bool Delete(int id);
}
