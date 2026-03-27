namespace TodoList.Web.Models;

public class TodoDashboardViewModel
{
    public int TotalTodos { get; set; }
    public int CompletedTodos { get; set; }
    public int OverdueTodos { get; set; }
    public Dictionary<string, int> TeamWorkload { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}
