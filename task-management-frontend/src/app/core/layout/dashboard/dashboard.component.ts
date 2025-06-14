import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListComponent } from '../../../features/task/components/task-list/task-list.component';
import { TaskFormComponent } from '../../../features/task/components/task-form/task-form.component';
import { TaskItem } from '../../../features/task/interfaces/task';
import { TaskService } from '../../../features/task/services/task.service';
import { ToastrService } from 'ngx-toastr';
import { HeaderComponent } from '../header/header.component';
import { UserContext } from '../../auth/interfaces/auth';
import { AuthService } from '../../auth/auth.service';

@Component({
  standalone: true,
  selector: 'app-dashboard',
  imports: [CommonModule, TaskListComponent, TaskFormComponent, HeaderComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {
  selectedTask?: TaskItem;

  tasks: TaskItem[] = [];
  username: string | null = null;
  constructor(private taskService: TaskService, private toastr: ToastrService, private authService: AuthService) {}
  async ngOnInit() {
    this.loadTasks();
    const userContext = await this.authService.userContext();
    this.username = userContext.username;
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
