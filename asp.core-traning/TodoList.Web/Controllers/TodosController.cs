using Microsoft.AspNetCore.Mvc;
using TodoList.Web.Models;
using TodoList.Web.Services;

namespace TodoList.Web.Controllers;

public class TodosController : Controller
{
    private readonly ITodoStore _todoStore;

    public TodosController(ITodoStore todoStore)
    {
        _todoStore = todoStore;
    }

    [HttpGet]
    public IActionResult Index(string? filter)
    {
        ViewBag.Filter = filter ?? "all";
        return View(_todoStore.GetByFilter(filter));
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var todo = _todoStore.GetById(id);
        return todo is null ? NotFound() : View(todo);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new TodoItemViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TodoItemViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var todo = _todoStore.Add(model);
        TempData["Message"] = $"Saved {todo.Title}";
        return RedirectToAction(nameof(Index));
    }
}
