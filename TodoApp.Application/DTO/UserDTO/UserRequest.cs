
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Application.DTO.UserDTO;

public class UserRequest
{
    public string UserName { get; set; }

    public string Password { get; set; }
}

