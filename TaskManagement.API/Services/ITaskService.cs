using TaskManagement.API.Models;

namespace TaskManagement.API.Services;

public interface ITaskService
{
    Task<(int totalCount, List<TaskItem> items)> GetAllAsync(int pageNumber, int pageSize);
    Task<TaskItem?> GetByIdAsync(int id);
    Task AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(TaskItem task);
}
