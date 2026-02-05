using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDBContext _context;
    public TodoRepository(TodoDBContext context) { _context = context; }



    public async Task<IList<TodoTask>> GetAll() { return await _context.Tasks.ToListAsync(); }

    public async Task<TodoTask?> GetByIdAsync(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task AddAsync(TodoTask task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(TodoTask entity)
    {
        _context.Tasks.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(TodoTask task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }


    public async Task<IList<TodoTask>> GetAllByUserIdAsync(int userId)
    {
        return await _context.Tasks
        
        .Where(t => t.UserId == userId)
        .OrderByDescending(t => t.CreatedAt)
        .ToListAsync();
    }

}