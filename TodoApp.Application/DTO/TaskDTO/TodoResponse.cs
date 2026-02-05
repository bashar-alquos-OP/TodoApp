using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Application.DTO.TaskDTO;
public class TodoResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
}

