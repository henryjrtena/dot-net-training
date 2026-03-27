using Microsoft.AspNetCore.Mvc;
using TodoList.Web.Services;

namespace TodoList.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class TodoDashboardController : Controller
{
    private readonly ITodoStore _todoStore;

    public TodoDashboardController(ITodoStore todoStore)
    {
        _todoStore = todoStore;
    }

    public IActionResult Index()
    {
        return View(_todoStore.GetDashboard());
    }
}
