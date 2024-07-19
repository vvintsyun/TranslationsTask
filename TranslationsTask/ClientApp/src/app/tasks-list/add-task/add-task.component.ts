import { Component, Inject, OnInit, inject} from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Task } from '../../models/task';
import { DateAdapter } from '@angular/material/core';
import { TasksService } from '../../services/tasks.service';
import { KeyValue } from '@angular/common';
import { forkJoin } from 'rxjs';
import * as moment from 'moment';
import { MomentDateAdapter } from '@angular/material-moment-adapter';

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.sass'],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
    },
  ]
})
export class AddTaskComponent implements OnInit {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: { projectId: number, taskId: number },
    private tasksService: TasksService
  ) {
    if (this.data?.taskId) {
      this.tasksService.getById(this.data.taskId).subscribe(x => {
        this.taskForm = new FormGroup({
          title: new FormControl(x.title),
          description: new FormControl(x.description),
          deadline: new FormControl(new Date(x.deadline)),
          assignee: new FormControl(x.assigneeId),
          project: new FormControl({ value: x.projectId, disabled: true }, Validators.required),
        });
      });

    } else {
      this.taskForm = new FormGroup({
        title: new FormControl(''),
        description: new FormControl(''),
        deadline: new FormControl(null),
        assignee: new FormControl(null),
        project: new FormControl({ value: this.data?.projectId, disabled: this.data && this.data.projectId > 0 }, Validators.required),
      });
    }    
  }

  ngOnInit(): void {
    this.loadDropdownData();    
  }

  loadDropdownData() {
    const $translators = this.tasksService.getTranslators();
    const $projects = this.tasksService.getProjects();

    forkJoin([$translators, $projects])
      .subscribe(([translators, projects]) => {
        this.projects = projects;
        this.translators = [{ key: null, value: '' }, ...translators];
      })
  }
  
  readonly dialogRef = inject(MatDialogRef<AddTaskComponent>);
  taskForm: FormGroup = new FormGroup({
    title: new FormControl(''),
    description: new FormControl(''),
    deadline: new FormControl(null),
    assignee: new FormControl(null),
    project: new FormControl(null, Validators.required)
  });
  translators: KeyValue<number, string>[] = [];
  projects: KeyValue<number, string>[] = [];

  getErrorMessage(formControl: any) {
    if (formControl.hasError('required')) {
      return 'You must enter a value';
    }

    return '';
  }

  onSubmit() {
    const data = {
      deadline: moment(this.taskForm.controls['deadline'].value).format('MM/DD/YYYY'),
      description: this.taskForm.controls['description'].value,
      title: this.taskForm.controls['title'].value,
      projectId: this.taskForm.controls['project'].value,
      assigneeId: this.taskForm.controls['assignee'].value,
      id: this.data?.taskId || 0
    } as Task;
    this.dialogRef.close(data);
  }
}
