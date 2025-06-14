using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.DTOs;
using TaskManagement.API.Models;
using TaskManagement.API.Services;

namespace TaskManagement.API.Controllers;

[Authorize(AuthenticationSchemes = "MyCookieAuth")]
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TasksController> _logger;
    public TasksController(ITaskService taskService, ILogger<TasksController> logger)
    {
        _taskService = taskService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Fetching tasks: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);
        var (totalCount, items) = await _taskService.GetAllAsync(pageNumber, pageSize);
        return Ok(new { totalCount, items });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("Fetching task with ID {Id}", id);
        var task = await _taskService.GetByIdAsync(id);
        if (task == null)
        {
            _logger.LogWarning("Task with ID {Id} not found", id);
            return NotFound();
        }

        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskItemDto dto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid task creation attempt");
            return BadRequest(ModelState);
        }

        try
        {
            var task = await _taskService.CreateAsync(dto);
            _logger.LogInformation("Created task with ID {Id}", task.Id);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a task");
            return StatusCode(500, "An error occurred while creating the task.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TaskItemDto dto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid update data for task ID {Id}", id);
            return BadRequest(ModelState);
        }

        try
        {
            var success = await _taskService.UpdateAsync(id, dto);
            if (!success)
            {
                _logger.LogWarning("Task with ID {Id} not found for update", id);
                return NotFound(new { message = $"Task with ID {id} not found." });
            }

            _logger.LogInformation("Updated task with ID {Id}", id);
            return Ok(new { message = "Task updated successfully." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating task ID {Id}", id);
            return StatusCode(500, "An error occurred while updating the task.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _taskService.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning("Task with ID {Id} not found for deletion", id);
                return NotFound(new { message = $"Task with ID {id} not found." });
            }

            _logger.LogInformation("Deleted task with ID {Id}", id);
            return Ok(new { message = "Task deleted successfully." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting task ID {Id}", id);
            return StatusCode(500, "An error occurred while deleting the task.");
        }
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> MarkAsCompleted(int id)
    {
        try
        {
            var task = await _taskService.MarkAsCompletedAsync(id);
            if (task == null)
            {
                _logger.LogWarning("Task with ID {TaskId} not found", id);
                return NotFound(new { message = $"Task with ID {id} not found." });
            }

            _logger.LogInformation("Task ID {TaskId} marked as completed", id);
            return Ok(new { message = "Task is completed.", task });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while marking as complete for task ID {TaskId}", id);
            return StatusCode(500, "An error occurred while marking task as completed");
        }
    }
}
