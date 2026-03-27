using TodoList.Web.Models;

namespace TodoList.Web.Services;

public interface ITodoStore
{
    IReadOnlyList<TodoItemViewModel> GetAll();
    IReadOnlyList<TodoItemViewModel> GetByFilter(string? filter);
    TodoItemViewModel? GetById(int id);
    TodoDashboardViewModel GetDashboard();
    TodoItemViewModel Add(TodoItemViewModel model);
    int GetActiveCount();
}
