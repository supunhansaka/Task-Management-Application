using TaskManagement.API.Models;

namespace TaskManagement.API.Repositories;

public interface ITaskRepository
{
    Task<int> GetTotalCountAsync();
    Task<List<TaskItem>> GetAllAsync(int pageNumber, int pageSize);
    Task<TaskItem?> GetByIdAsync(int id);
    Task AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(TaskItem task);
}

