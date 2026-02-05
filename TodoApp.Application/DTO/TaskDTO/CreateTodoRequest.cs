using System.ComponentModel.DataAnnotations;


namespace TodoApp.Application.DTO.TaskDTO;
public class CreateTodoRequest
{
    [Required]
    [MaxLength(20)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Content { get; set; } = string.Empty;
}

