import { TaskPriority, TaskStatus } from '../enums/task.enums';

export interface TaskItem {
  id: number;
  title: string;
  description?: string;
  status: TaskStatus;
  priority: TaskPriority;
  dueDate: string;
  createdAt: string;
  updatedAt?: string;
}
