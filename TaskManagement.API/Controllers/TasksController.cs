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

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (totalCount, items) = await _taskService.GetAllAsync(pageNumber, pageSize);

        return Ok(new
        {
            totalCount,
            items
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskItemDto dto)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = dto.IsCompleted
        };

        await _taskService.AddAsync(task);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskItemDto dto)
    {
        var existingTask = await _taskService.GetByIdAsync(id);
        if (existingTask == null) return NotFound();

        existingTask.Title = dto.Title;
        existingTask.Description = dto.Description;
        existingTask.IsCompleted = dto.IsCompleted;
        existingTask.UpdatedAt = DateTime.UtcNow;

        await _taskService.UpdateAsync(existingTask);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task == null) return NotFound();

        await _taskService.DeleteAsync(task);
        return NoContent();
    }
}
