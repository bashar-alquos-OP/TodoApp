using System.ComponentModel.DataAnnotations;


namespace TodoApp.Application.DTO.UserDTO;
public class UserDataResponse
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

