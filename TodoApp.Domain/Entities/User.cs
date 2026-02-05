using Microsoft.AspNetCore.Identity;

namespace TodoApp.Domain.Entities;

public class User : IdentityUser<int>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<TodoTask>? Tasks { get; set; } = new List<TodoTask>();

}

