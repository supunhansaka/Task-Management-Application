using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Data;
using TaskManagement.API.Models;

namespace TaskManagement.API.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _dbContext;

    public TaskRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetTotalCountAsync()
    {
        return await _dbContext.Tasks.CountAsync();
    }

    public async Task<List<TaskItem>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _dbContext.Tasks
            .OrderBy(t => t.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync() =>
        await _dbContext.Tasks.ToListAsync();

    public async Task<TaskItem?> GetByIdAsync(int id) =>
        await _dbContext.Tasks.FindAsync(id);

    public async Task AddAsync(TaskItem task)
    {
        _dbContext.Tasks.Add(task);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskItem task)
    {
        _dbContext.Tasks.Update(task);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TaskItem task)
    {
        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync();
    }
}
