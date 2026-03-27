using Microsoft.AspNetCore.Mvc;
using TodoList.Web.Services;

namespace TodoList.Web.ViewComponents;

public class TodoSummaryViewComponent : ViewComponent
{
    private readonly ITodoStore _todoStore;

    public TodoSummaryViewComponent(ITodoStore todoStore)
    {
        _todoStore = todoStore;
    }

    public IViewComponentResult Invoke()
    {
        return View("Default", _todoStore.GetActiveCount());
    }
}
