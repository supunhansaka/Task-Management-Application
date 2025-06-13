import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TaskService } from '../../services/task.service';
import { TaskItem } from '../../interfaces/task';
import { ToastrService } from 'ngx-toastr';

@Component({
  standalone: true,
  selector: 'app-task-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.scss']
})
export class TaskFormComponent implements OnChanges {
  @Input() taskToEdit?: TaskItem;

  taskForm: FormGroup;

  constructor(private fb: FormBuilder, private taskService: TaskService, private toastr: ToastrService) {
    this.taskForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      isCompleted: [false]
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['taskToEdit'] && this.taskToEdit) {
      this.taskForm.patchValue({
        title: this.taskToEdit.title,
        description: this.taskToEdit.description,
        isCompleted: this.taskToEdit.isCompleted
      });
    }
  }

  async onSubmit() {
    if (this.taskForm.valid) {
      if (this.taskToEdit) {
        await this.taskService.updateTask(this.taskToEdit.id, this.taskForm.value);
        this.toastr.success('Task updated successfully');
      } else {
        await this.taskService.addTask(this.taskForm.value);
        this.toastr.success('Task added successfully');
      }

      this.taskForm.reset();
      this.taskToEdit = undefined;
    }
  }
}
