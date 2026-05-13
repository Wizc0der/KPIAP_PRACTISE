using Microsoft.AspNetCore.Mvc;
using PhoneBook.Data;
using PhoneBook.Models;

namespace PhoneBook.Controllers;

public class ContactsController : Controller
{
    private readonly ContactRepository _repo;

    public ContactsController(ContactRepository repo)
    {
        _repo = repo;
    }

    // GET /Contacts  или  GET /Contacts/Index
    public IActionResult Index()
    {
        var contacts = _repo.GetAll();
        return View(contacts);
    }

    // GET /Contacts/Search/{name}
    [HttpGet("Contacts/Search/{name?}")]
    public IActionResult Search(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return RedirectToAction(nameof(Index));

        var results = _repo.Search(name);
        ViewBag.SearchName = name;
        return View("Index", results);
    }

    // GET /Contacts/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST /Contacts/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Contact contact)
    {
        if (!ModelState.IsValid)
            return View(contact);

        _repo.Add(contact);
        TempData["Success"] = $"Контакт «{contact.Name}» успешно добавлен!";
        return RedirectToAction(nameof(Index));
    }

    // POST /Contacts/Delete/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _repo.Delete(id);
        TempData["Success"] = "Контакт удалён.";
        return RedirectToAction(nameof(Index));
    }
}
