using TodoApp.Domain.Entities;

public interface ITodoRepository
{
    Task<IList<TodoTask>> GetAll();
    Task AddAsync(TodoTask task);
    Task DeleteAsync(TodoTask entity);
    Task<TodoTask?> GetByIdAsync(int id);
    Task UpdateAsync(TodoTask task);
    Task<IList<TodoTask>> GetAllByUserIdAsync(int userId);
}