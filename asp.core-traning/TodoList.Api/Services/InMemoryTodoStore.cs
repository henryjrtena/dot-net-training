using TodoList.Api.Models;

namespace TodoList.Api.Services;

public class InMemoryTodoStore : ITodoStore
{
    private readonly List<TodoItemDto> _todos =
    [
        new() { Id = 1, Title = "Learn Web API", AssignedTo = "Henry", IsDone = false },
        new() { Id = 2, Title = "Connect React app", AssignedTo = "Team", IsDone = true }
    ];

    public IReadOnlyList<TodoItemDto> GetAll()
    {
        return _todos.OrderBy(todo => todo.Id).ToList();
    }

    public TodoItemDto? GetById(int id)
    {
        return _todos.FirstOrDefault(todo => todo.Id == id);
    }

    public TodoItemDto Add(TodoItemDto model)
    {
        var todo = new TodoItemDto
        {
            Id = _todos.Max(todo => todo.Id) + 1,
            Title = model.Title.Trim(),
            AssignedTo = model.AssignedTo.Trim(),
            IsDone = model.IsDone
        };

        _todos.Add(todo);
        return todo;
    }
}
