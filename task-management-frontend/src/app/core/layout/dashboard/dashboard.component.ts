import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListComponent } from '../../../features/task/components/task-list/task-list.component';
import { TaskFormComponent } from '../../../features/task/components/task-form/task-form.component';
import { TaskItem } from '../../../features/task/interfaces/task';

@Component({
  standalone: true,
  selector: 'app-dashboard',
  imports: [CommonModule, TaskListComponent, TaskFormComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {
  selectedTask?: TaskItem;
}
