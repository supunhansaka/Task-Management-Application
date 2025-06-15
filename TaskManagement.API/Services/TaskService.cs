using Microsoft.VisualBasic;
using TaskManagement.API.Authentication;
using TaskManagement.API.DTOs;
using TaskManagement.API.Enums;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories;
using TaskStatus = TaskManagement.API.Enums.TaskStatus;

namespace TaskManagement.API.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IAuthContext _authContext;

    public TaskService(ITaskRepository repository, IAuthContext authContext)
    {
        _repository = repository;
        _authContext = authContext;
    }

    public async Task<(int totalCount, List<TaskItem> items)> GetAllAsync(int pageNumber, int pageSize)
    {
        if (!_authContext.UserId.HasValue)
            throw new UnauthorizedAccessException();

        var totalCount = await _repository.GetTotalCountAsync(_authContext.UserId.Value);
        var items = await _repository.GetAllAsync(pageNumber, pageSize, _authContext.UserId.Value);
        return (totalCount, items);
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        if (!_authContext.UserId.HasValue)
            throw new UnauthorizedAccessException();

        var task = await _repository.GetByIdAsync(id);
        return (task != null && task.UserId == _authContext.UserId.Value) ? task : null;
    }

    public async Task<TaskItem> CreateAsync(TaskItemDto dto)
    {
        if (!_authContext.UserId.HasValue)
            throw new UnauthorizedAccessException();

        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Status = TaskStatus.Open,
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            CreatedAt = DateTime.UtcNow,
            UserId = _authContext.UserId.Value
        };

        await _repository.AddAsync(task);
        return task;
    }

    public async Task<bool> UpdateAsync(int id, TaskItemDto dto)
    {
        if (!_authContext.UserId.HasValue)
            throw new UnauthorizedAccessException();

        var existingTask = await _repository.GetByIdAsync(id);
        if (existingTask == null || existingTask.UserId != _authContext.UserId.Value) return false;

        existingTask.Title = dto.Title;
        existingTask.Description = dto.Description;
        existingTask.Status = dto.Status;
        existingTask.Priority = dto.Priority;
        existingTask.DueDate = dto.DueDate;
        existingTask.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existingTask);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (!_authContext.UserId.HasValue)
            throw new UnauthorizedAccessException();

        var task = await _repository.GetByIdAsync(id);
        if (task == null || task.UserId != _authContext.UserId.Value) return false;

        await _repository.DeleteAsync(task);
        return true;
    }

    public async Task<TaskItem?> MarkAsCompletedAsync(int id)
    {
        if (!_authContext.UserId.HasValue)
            throw new UnauthorizedAccessException();

        var task = await _repository.GetByIdAsync(id);
        if (task == null || task.UserId != _authContext.UserId.Value) return null;

        if (task.Status != TaskStatus.Completed)
        {
            task.Status = TaskStatus.Completed;
            task.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(task);
        }

        return task;
    }
}
