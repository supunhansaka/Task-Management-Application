using TaskManagement.API.Models;

namespace TaskManagement.API.Repositories;

public interface ITaskRepository
{
    Task<int> GetTotalCountAsync(int userId);
    Task<List<TaskItem>> GetAllAsync(int pageNumber, int pageSize, int userId);
    Task<TaskItem?> GetByIdAsync(int id);
    Task AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(TaskItem task);
}

