import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormControl } from '@angular/forms';
import { TaskService } from '../../services/task.service';
import { TaskItem } from '../../interfaces/task';
import { ToastrService } from 'ngx-toastr';

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
  Math = Math; // Add Math for template use

  filterControl = new FormControl('');
  @Input() tasks: TaskItem[] = [];
  @Output() taskSelected = new EventEmitter<TaskItem>();
  @Output() refresh = new EventEmitter<void>();

  constructor(private taskService: TaskService, private toastr: ToastrService) {}

  async ngOnInit() {
    await this.loadTasks();

    // Optional → live filter
    this.filterControl.valueChanges.subscribe(() => {
      // trigger change detection if needed (or use getter)
    });
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
    this.tasks.sort((a, b) =>
      this.sortAscending
        ? a.title.localeCompare(b.title)
        : b.title.localeCompare(a.title)
    );
  }

  get filteredTasks() {
    const filter = this.filterControl.value?.toLowerCase() || '';
    return this.tasks.filter(task =>
      task.title.toLowerCase().includes(filter)
    );
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
