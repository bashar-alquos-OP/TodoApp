using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Application.DTO.TaskDTO;

public class UpdateTodoRequest
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public bool IsCompleted { get; set; }
}

