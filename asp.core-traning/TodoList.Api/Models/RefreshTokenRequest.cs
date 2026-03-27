using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Models;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
