using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}