using TodoList.Web.Models;

namespace TodoList.Web.Services;

public class InMemoryTodoStore : ITodoStore
{
    private readonly List<TodoItemViewModel> _todos =
    [
        new()
        {
            Id = 1,
            Title = "Learn MVC",
            AssignedTo = "Henry",
            DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-1))
        },
        new()
        {
            Id = 2,
            Title = "Create Todo form",
            AssignedTo = "Team",
            DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(2))
        },
        new()
        {
            Id = 3,
            Title = "Connect Admin dashboard",
            AssignedTo = "Ava",
            IsDone = true,
            DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-2))
        }
    ];

    public IReadOnlyList<TodoItemViewModel> GetAll()
    {
        return _todos
            .OrderBy(todo => todo.IsDone)
            .ThenBy(todo => todo.DueDate)
            .ThenBy(todo => todo.Id)
            .ToList();
    }

    public IReadOnlyList<TodoItemViewModel> GetByFilter(string? filter)
    {
        return (filter ?? "all").ToLowerInvariant() switch
        {
            "active" => GetAll().Where(todo => !todo.IsDone).ToList(),
            "done" => GetAll().Where(todo => todo.IsDone).ToList(),
            "overdue" => GetAll().Where(todo => !todo.IsDone && todo.DueDate < DateOnly.FromDateTime(DateTime.Today)).ToList(),
            _ => GetAll()
        };
    }

    public TodoItemViewModel? GetById(int id)
    {
        return _todos.FirstOrDefault(todo => todo.Id == id);
    }

    public TodoDashboardViewModel GetDashboard()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        return new TodoDashboardViewModel
        {
            TotalTodos = _todos.Count,
            CompletedTodos = _todos.Count(todo => todo.IsDone),
            OverdueTodos = _todos.Count(todo => !todo.IsDone && todo.DueDate < today),
            TeamWorkload = _todos
                .GroupBy(todo => string.IsNullOrWhiteSpace(todo.AssignedTo) ? "Unassigned" : todo.AssignedTo)
                .OrderBy(group => group.Key)
                .ToDictionary(group => group.Key, group => group.Count())
        };
    }

    public TodoItemViewModel Add(TodoItemViewModel model)
    {
        var todo = new TodoItemViewModel
        {
            Id = _todos.Max(todo => todo.Id) + 1,
            Title = model.Title.Trim(),
            AssignedTo = model.AssignedTo.Trim(),
            DueDate = model.DueDate,
            IsDone = model.IsDone
        };

        _todos.Add(todo);
        return todo;
    }

    public int GetActiveCount()
    {
        return _todos.Count(todo => !todo.IsDone);
    }
}
