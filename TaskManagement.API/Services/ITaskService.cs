using TaskManagement.API.DTOs;
using TaskManagement.API.Models;

namespace TaskManagement.API.Services;

public interface ITaskService
{
    Task<(int totalCount, List<TaskItem> items)> GetAllAsync(int pageNumber, int pageSize);
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(TaskItemDto dto);
    Task<bool> UpdateAsync(int id, TaskItemDto dto);
    Task<bool> DeleteAsync(int id);
    Task<TaskItem?> MarkAsCompletedAsync(int id);
}
