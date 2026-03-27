using TodoList.Api.Models;

namespace TodoList.Api.Services;

public interface ITodoStore
{
    IReadOnlyList<TodoItemDto> GetAll();
    TodoItemDto? GetById(int id);
    TodoItemDto Add(TodoItemDto model);
}
