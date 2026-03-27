using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TodoList.Web.Models;
using TodoList.Web.Services;

namespace TodoList.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITodoStore _todoStore;

    public HomeController(ILogger<HomeController> logger, ITodoStore todoStore)
    {
        _logger = logger;
        _todoStore = todoStore;
    }

    public IActionResult Index()
    {
        ViewBag.ActiveCount = _todoStore.GetActiveCount();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
