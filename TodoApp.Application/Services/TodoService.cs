
using TodoApp.Application.DTO.TaskDTO;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services;


public class TodoService : ITodoService
{
    private readonly ITodoRepository _repo;

    public TodoService(ITodoRepository repository)
    {
        _repo = repository;
    }


    public async Task<IEnumerable<TodoResponse>> GetUserTasksAsync(int userId)
    {
        var tasks = await _repo.GetAllByUserIdAsync(userId);

        return tasks.Select(t => new TodoResponse
        {
            Id = t.Id,
            Title = t.Title,
            Content = t.Content,
            IsCompleted = t.IsCompleted,
            CreatedAt = t.CreatedAt
        });
    }

    public async Task<TodoResponse?> GetTaskByIdAsync(int id, int userId)
    {
        var task = await _repo.GetByIdAsync(id);

        if (task == null || task.UserId != userId)
            return null;

        return new TodoResponse
        {
            Id = task.Id,
            Title = task.Title,
            Content = task.Content,
            IsCompleted = task.IsCompleted,
            CreatedAt = task.CreatedAt
        };
    }

    public async Task<TodoResponse> CreateTaskAsync(CreateTodoRequest request, int userId)
    {
        var task = new TodoTask
        {
            Title = request.Title,
            Content = request.Content,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(task);

        return new TodoResponse
        {
            Id = task.Id,
            Title = task.Title,
            Content = task.Content,
            IsCompleted = task.IsCompleted,
            CreatedAt = task.CreatedAt
        };
    }


    public async Task<bool> UpdateTaskAsync(int id, UpdateTodoRequest request, int userId){
        var task = await _repo.GetByIdAsync(id);

        if (task == null || task.UserId != userId)
            return false;
        
        task.Title = request.Title ?? task.Title;
        task.Content = request.Content ?? task.Content;
        task.IsCompleted = request.IsCompleted;

        await _repo.UpdateAsync(task);
        return true;
    }


    public async Task<bool> DeleteTaskAsync(int id, int userId) {
        var task = await _repo.GetByIdAsync(id);

        if (task == null || task.UserId != userId)
            return false;

        await _repo.DeleteAsync(task);
        return true;
    }
}
