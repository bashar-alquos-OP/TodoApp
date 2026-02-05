using System.ComponentModel.DataAnnotations;


namespace TodoApp.Application.DTO.UserDTO;

public class UpdateUserRequest
{
    public string? UserName { get; set; }
    public string? Password { get; set; }

}

