using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Models;

public class TodoItemDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [StringLength(40)]
    public string AssignedTo { get; set; } = string.Empty;

    public bool IsDone { get; set; }
}
