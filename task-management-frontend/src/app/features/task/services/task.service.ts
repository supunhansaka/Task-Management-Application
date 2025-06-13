import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { TaskItem } from '../interfaces/task';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getTasks(pageNumber: number, pageSize: number): Promise<{ totalCount: number; items: TaskItem[] }> {
    return firstValueFrom(
      this.http.get<{ totalCount: number; items: TaskItem[] }>(
        `${this.apiUrl}/tasks`,
        {
          params: {
            pageNumber: pageNumber.toString(),
            pageSize: pageSize.toString()
          },
          withCredentials: true
        }
      )
    );
  }

  getTask(id: number): Promise<TaskItem> {
    return firstValueFrom(
      this.http.get<TaskItem>(`${this.apiUrl}/tasks/${id}`, { withCredentials: true })
    );
  }

  addTask(task: Partial<TaskItem>): Promise<TaskItem> {
    return firstValueFrom(
      this.http.post<TaskItem>(`${this.apiUrl}/tasks`, task, { withCredentials: true })
    );
  }

  updateTask(id: number, task: Partial<TaskItem>): Promise<void> {
    return firstValueFrom(
      this.http.put<void>(`${this.apiUrl}/tasks/${id}`, task, { withCredentials: true })
    );
  }

  deleteTask(id: number): Promise<void> {
    return firstValueFrom(
      this.http.delete<void>(`${this.apiUrl}/tasks/${id}`, { withCredentials: true })
    );
  }
}
