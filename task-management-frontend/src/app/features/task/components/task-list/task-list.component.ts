import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormControl } from '@angular/forms';
import { TaskService } from '../../services/task.service';
import { TaskItem } from '../../interfaces/task';

@Component({
  standalone: true,
  selector: 'app-task-list',
  imports: [CommonModule, ReactiveFormsModule],  // Only ReactiveFormsModule now!
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent implements OnInit {
  currentPage = 1;
  pageSize = 10;
  totalCount = 0;
  totalPages = 0;
  tasks: TaskItem[] = [];
  sortAscending = true;

  filterControl = new FormControl('');
  @Output() taskSelected = new EventEmitter<TaskItem>();

  constructor(private taskService: TaskService) {}

  async ngOnInit() {
    await this.loadTasks();

    // Optional → live filter
    this.filterControl.valueChanges.subscribe(() => {
      // trigger change detection if needed (or use getter)
    });
  }

  async loadTasks() {
    const result = await this.taskService.getTasks(this.currentPage, this.pageSize);
    this.tasks = result.items;
    this.totalCount = result.totalCount;
  }

  goToPage(page: number) {
    this.currentPage = page;
    this.loadTasks();
  }
  
  selectTask(task: TaskItem) {
    this.taskSelected.emit(task);
  }

  async deleteTask(id: number) {
    await this.taskService.deleteTask(id);
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
}
