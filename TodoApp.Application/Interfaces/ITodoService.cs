
using TodoApp.Application.DTO.TaskDTO;

namespace TodoApp.Application.Interfaces;

public interface ITodoService
{
    Task<IEnumerable<TodoResponse>> GetUserTasksAsync(int userId);
    Task<TodoResponse?> GetTaskByIdAsync(int id, int userId);
    Task<TodoResponse> CreateTaskAsync(CreateTodoRequest request, int userId);
    Task<bool> UpdateTaskAsync(int id, UpdateTodoRequest request, int userId);
    Task<bool> DeleteTaskAsync(int id, int userId);
}

