using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApp.Application.DTO.TaskDTO;
using TodoApp.Application.Interfaces;


namespace TodoApp.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITodoService _service;

    public TaskController(ITodoService service)
    { _service = service; }

    private int GetCurrentUserId() =>
        int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);


    [HttpGet()]
    public async Task<IActionResult> GetTasks()
    {
        var userId = GetCurrentUserId();
        var data = (await _service.GetUserTasksAsync(userId));

        return data.Any() ? Ok(data) : NoContent();
    }

    [HttpPost()]
    public async Task<IActionResult> CreateTask(CreateTodoRequest request) 
    {
        var userId = GetCurrentUserId();
        var newUser = await _service.CreateTaskAsync(request, userId);

        if (newUser == null)
            return BadRequest("Something went wrong");

        return Created("", newUser);
    }


    [HttpPatch("{taskId}")]
    public async Task<IActionResult> UpdateTask(int taskId, UpdateTodoRequest request) {

        var userId = GetCurrentUserId();
        bool isUpdated = await _service.UpdateTaskAsync(taskId, request, userId);

        if(isUpdated)
            return Ok("Task has been updated");
        
      return NotFound("Task not found");
    }


    [HttpDelete("{taskId}")]
    public async Task<IActionResult> DeleteTask(int taskId) {
        var userId = GetCurrentUserId();
        bool isDeleted = await _service.DeleteTaskAsync(taskId, userId);
        if (isDeleted)
            return Ok("Task has been deleted");
        
        return NotFound("Task not found");
    }

}