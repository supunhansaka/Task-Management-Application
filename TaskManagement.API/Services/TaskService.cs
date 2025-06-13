using TaskManagement.API.Models;
using TaskManagement.API.Repositories;

namespace TaskManagement.API.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<(int totalCount, List<TaskItem> items)> GetAllAsync(int pageNumber, int pageSize)
    {
        var totalCount = await _repository.GetTotalCountAsync();
        var items = await _repository.GetAllAsync(pageNumber, pageSize);

        return (totalCount, items);
    }

    public Task<TaskItem?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task AddAsync(TaskItem task) => _repository.AddAsync(task);
    public Task UpdateAsync(TaskItem task) => _repository.UpdateAsync(task);
    public Task DeleteAsync(TaskItem task) => _repository.DeleteAsync(task);
}
