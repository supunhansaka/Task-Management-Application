using System.ComponentModel.DataAnnotations;
using TaskManagement.API.Enums;
using TaskStatus = TaskManagement.API.Enums.TaskStatus;
using TaskPriority = TaskManagement.API.Enums.TaskPriority;

namespace TaskManagement.API.DTOs;

public class TaskItemDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [StringLength(300)]
    public string? Description { get; set; }

    [Required]
    public TaskStatus Status { get; set; } = TaskStatus.Open;

    [Required]
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    [Required]
    public DateTime DueDate { get; set; }
}
