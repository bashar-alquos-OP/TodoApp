using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

public class TodoDBContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public TodoDBContext(DbContextOptions<TodoDBContext> options) : base(options) { }


    public DbSet<TodoTask> Tasks { get; set; }

}

