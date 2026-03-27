using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Models;

public class TodoItemViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [StringLength(40)]
    public string AssignedTo { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateOnly DueDate { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

    public bool IsDone { get; set; }
}
