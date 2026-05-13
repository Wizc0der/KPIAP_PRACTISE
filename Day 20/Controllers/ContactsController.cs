using Microsoft.AspNetCore.Mvc;
using PhoneBook.Services;
using PhoneBook.ViewModels;

namespace PhoneBook.Controllers;

public class ContactsController : Controller
{
    // ── DI: зависимость от интерфейса, не от конкретной реализации ──────────
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    // ────────────────────────────────────────────────────────────────────────
    // GET /Contacts
    // ────────────────────────────────────────────────────────────────────────
    public IActionResult Index()
    {
        var contacts = _contactService.GetAll();

        // ViewBag — передаём сообщение из TempData во ViewBag,
        // чтобы вьюшка работала только с ViewBag (по заданию).
        if (TempData["Message"] is string msg)
            ViewBag.Message = msg;

        return View(contacts);
    }

    // ────────────────────────────────────────────────────────────────────────
    // GET /Contacts/Search/{name}
    // ────────────────────────────────────────────────────────────────────────
    [HttpGet("Contacts/Search/{name?}")]
    public IActionResult Search(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return RedirectToAction(nameof(Index));

        var results = _contactService.Search(name);

        ViewBag.SearchName = name;
        ViewBag.Message    = $"Результаты поиска по запросу «{name}»: найдено {results.Count()} контакт(ов).";

        return View("Index", results);
    }

    // ────────────────────────────────────────────────────────────────────────
    // GET /Contacts/Create
    // ────────────────────────────────────────────────────────────────────────
    public IActionResult Create()
    {
        return View(new ContactViewModel());
    }

    // ────────────────────────────────────────────────────────────────────────
    // POST /Contacts/Create
    // ────────────────────────────────────────────────────────────────────────
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ContactViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            // Сообщение об ошибке — тоже через ViewBag
            ViewBag.Message     = "⚠ Пожалуйста, исправьте ошибки в форме.";
            ViewBag.MessageType = "error";
            return View(viewModel);
        }

        var added = _contactService.Add(viewModel);

        // Сохраняем в TempData → после редиректа переносим в ViewBag в Index()
        TempData["Message"] = $"✓ Контакт «{added.Name}» успешно добавлен!";
        return RedirectToAction(nameof(Index));
    }

    // ────────────────────────────────────────────────────────────────────────
    // POST /Contacts/Delete/{id}
    // ────────────────────────────────────────────────────────────────────────
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _contactService.Delete(id);
        TempData["Message"] = "🗑 Контакт удалён.";
        return RedirectToAction(nameof(Index));
    }
}
