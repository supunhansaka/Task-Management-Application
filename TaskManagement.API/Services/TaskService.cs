using TaskManagement.API.Models;
using TaskManagement.API.Repositories;

namespace TaskManagement.API.Services;

public class TaskService
{
    private readonly TaskRepository _repository;

    public TaskService(TaskRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<TaskItem>> GetAllAsync() => _repository.GetAllAsync();
    public Task<TaskItem?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
    public Task AddAsync(TaskItem task) => _repository.AddAsync(task);
    public Task UpdateAsync(TaskItem task) => _repository.UpdateAsync(task);
    public Task DeleteAsync(TaskItem task) => _repository.DeleteAsync(task);
}
