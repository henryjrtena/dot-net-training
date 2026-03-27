using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Models;
using TodoList.Api.Services;

namespace TodoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoStore _todoStore;

    public TodosController(ITodoStore todoStore)
    {
        _todoStore = todoStore;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TodoItemDto>> Get()
    {
        return Ok(_todoStore.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<TodoItemDto> GetById(int id)
    {
        var todo = _todoStore.GetById(id);
        return todo is null ? NotFound() : Ok(todo);
    }

    [HttpPost]
    public ActionResult<TodoItemDto> Post(TodoItemDto model)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var todo = _todoStore.Add(model);
        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
    }

    [Authorize]
    [HttpGet("secure")]
    public IActionResult GetSecureTodos()
    {
        return Ok(new[]
        {
            new TodoItemDto
            {
                Id = 99,
                Title = "Finish auth flow",
                AssignedTo = User.Identity?.Name ?? "Authenticated user",
                IsDone = false
            }
        });
    }
}
