import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  SimpleChanges,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { TaskService } from '../../services/task.service';
import { TaskItem } from '../../interfaces/task';
import { ToastrService } from 'ngx-toastr';
import { TASK_PRIORITIES, TASK_STATUSES } from '../../enums/task.enums';

@Component({
  standalone: true,
  selector: 'app-task-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.scss'],
})
export class TaskFormComponent implements OnChanges {
  @Input() taskToEdit?: TaskItem;
  @Output() taskSaved = new EventEmitter<void>();
  @Output() formClosed = new EventEmitter<void>();

  taskStatuses = TASK_STATUSES;
  taskPriorities = TASK_PRIORITIES;

  taskForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
    private toastr: ToastrService
  ) {
    this.taskForm = this.fb.group({
      title: ['', Validators.required],
      description: [''],
      status: [TASK_STATUSES[0]],
      priority: [TASK_PRIORITIES[1], Validators.required],
      dueDate: [null, Validators.required],
    });
  }

  ngOnChanges() {
    if (this.taskToEdit) {
      this.taskForm.patchValue(this.taskToEdit);
    } else {
      this.taskForm.reset({ status: 'Open', priority: 'Medium' });
    }
  }

  async onSubmit() {
    if (this.taskForm.valid) {
      const data = {
        ...this.taskForm.value,
        status: this.taskForm.value.status || 'Open',
        priority: this.taskForm.value.priority || 'Medium',
      };

      if (this.taskToEdit) {
        try {
          await this.taskService.updateTask(this.taskToEdit.id, data);
          this.toastr.success('Task updated successfully');
          this.formClosed.emit();
        } catch (error) {
          this.toastr.error('Failed to update task');
        }
      } else {
        try {
          await this.taskService.addTask(data);
          this.toastr.success('Task added successfully');
        } catch (error) {
          this.toastr.error('Failed to add task');
        }
      }

      this.taskSaved.emit();
      this.taskForm.reset({ status: 'Open', priority: 'Medium' });
    }
  }

  cancelEdit() {
    this.taskForm.reset({ status: 'Open', priority: 'Medium' });
    this.formClosed.emit();
  }
}
