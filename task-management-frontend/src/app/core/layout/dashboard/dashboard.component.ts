import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListComponent } from '../../../features/task/components/task-list/task-list.component';
import { TaskFormComponent } from '../../../features/task/components/task-form/task-form.component';
import { TaskItem } from '../../../features/task/interfaces/task';
import { TaskService } from '../../../features/task/services/task.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  standalone: true,
  selector: 'app-dashboard',
  imports: [CommonModule, TaskListComponent, TaskFormComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {
  selectedTask?: TaskItem;

  tasks: TaskItem[] = [];
  constructor(private taskService: TaskService, private toastr: ToastrService) {}
  ngOnInit() {
    this.loadTasks();
  }

  async loadTasks() {
    try {
      const result = await this.taskService.getTasks(1, 5);
      this.tasks = result.items;
    } catch (error) {
      this.toastr.error('Failed to load tasks');
    }
  }

  onEditTask(task: TaskItem) {
    this.selectedTask = task;
  }

  onFormClosed() {
    this.selectedTask = undefined;
  }
}
