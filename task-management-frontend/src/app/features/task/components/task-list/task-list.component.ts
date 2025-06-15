import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormControl } from '@angular/forms';
import { TaskService } from '../../services/task.service';
import { TaskItem } from '../../interfaces/task';
import { ToastrService } from 'ngx-toastr';
import { TASK_PRIORITIES, TASK_STATUSES } from '../../enums/task.enums';

@Component({
  standalone: true,
  selector: 'app-task-list',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent implements OnInit {
  currentPage = 1;
  pageSize = 5;
  totalCount = 0;
  totalPages = 0;
  // tasks: TaskItem[] = [];
  sortAscending = true;
  currentSortField: 'title' | 'status' | 'priority' | 'dueDate' = 'title';
  Math = Math;

  taskStatuses = TASK_STATUSES;
  taskPriorities = TASK_PRIORITIES;

  private readonly statusOrder = TASK_STATUSES;
  private readonly priorityOrder = TASK_PRIORITIES;

  filterControl = new FormControl('');
  statusFilterControl = new FormControl('');
  priorityFilterControl = new FormControl('');

  @Input() tasks: TaskItem[] = [];
  @Output() taskSelected = new EventEmitter<TaskItem>();
  @Output() refresh = new EventEmitter<void>();

  constructor(private taskService: TaskService, private toastr: ToastrService) {}

  async ngOnInit() {
    await this.loadTasks();

    // Subscribe to all filter changes
    this.filterControl.valueChanges.subscribe(() => {
      this.resetPagination();
    });

    this.statusFilterControl.valueChanges.subscribe(() => {
      this.resetPagination();
    });

    this.priorityFilterControl.valueChanges.subscribe(() => {
      this.resetPagination();
    });
  }

  private resetPagination() {
    this.currentPage = 1;
    this.loadTasks();
  }

  async loadTasks() {
    try {
      const result = await this.taskService.getTasks(this.currentPage, this.pageSize);
      this.tasks = result.items;
      this.totalCount = result.totalCount;
    } catch (error) {
      this.toastr.error('Failed to load tasks');
    }
  }

  goToPage(page: number) {
    this.currentPage = page;
    this.loadTasks();
  }
  
  selectTask(task: TaskItem) {
    this.taskSelected.emit(task);
  }

  async deleteTask(id: number) {
    try {
      await this.taskService.deleteTask(id);
      this.toastr.success('Task deleted successfully');
    } catch (error) {
      this.toastr.error('Failed to delete task');
    }
    await this.loadTasks();
  }

  toggleSort() {
    this.sortAscending = !this.sortAscending;
    this.sortTasks();
  }

  setSortField(field: 'title' | 'status' | 'priority' | 'dueDate') {
    if (this.currentSortField === field) {
      this.sortAscending = !this.sortAscending;
    } else {
      this.currentSortField = field;
      this.sortAscending = true;
    }
    this.sortTasks();
  }

  private sortTasks() {
    switch (this.currentSortField) {
      case 'title':
        this.tasks.sort((a, b) =>
          this.sortAscending
            ? a.title.localeCompare(b.title)
            : b.title.localeCompare(a.title)
        );
        break;
      case 'status':
        this.tasks.sort((a, b) => {
          const aIndex = this.statusOrder.indexOf(a.status);
          const bIndex = this.statusOrder.indexOf(b.status);
          return this.sortAscending ? aIndex - bIndex : bIndex - aIndex;
        });
        break;
      case 'priority':
        this.tasks.sort((a, b) => {
          const aIndex = this.priorityOrder.indexOf(a.priority);
          const bIndex = this.priorityOrder.indexOf(b.priority);
          return this.sortAscending ? aIndex - bIndex : bIndex - aIndex;
        });
        break;
      case 'dueDate':
        this.tasks.sort((a, b) => {
          const dateA = new Date(a.dueDate).getTime();
          const dateB = new Date(b.dueDate).getTime();
          return this.sortAscending ? dateA - dateB : dateB - dateA;
        });
        break;
    }
  }

  get filteredTasks() {
    const searchFilter = this.filterControl.value?.toLowerCase() || '';
    const statusFilter = this.statusFilterControl.value || '';
    const priorityFilter = this.priorityFilterControl.value || '';

    return this.tasks.filter(task => {
      const matchesSearch = 
        task.title.toLowerCase().includes(searchFilter) ||
        task.status.toLowerCase().includes(searchFilter) ||
        task.priority.toLowerCase().includes(searchFilter);
      
      const matchesStatus = !statusFilter || task.status === statusFilter;
      const matchesPriority = !priorityFilter || task.priority === priorityFilter;

      return matchesSearch && matchesStatus && matchesPriority;
    });
  }

  onEdit(task: TaskItem) {
    this.taskSelected.emit(task);
  }
  
  async markAsCompleted(taskId: number) {
    try {
      await this.taskService.markAsCompleted(taskId);
      this.toastr.success('Task marked as completed');
      this.loadTasks();
    } catch {
      this.toastr.error('Failed to update task completion status.');
    }
  }
}
