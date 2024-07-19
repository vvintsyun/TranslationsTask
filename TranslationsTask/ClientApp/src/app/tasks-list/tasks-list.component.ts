import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { filter, switchMap, tap } from 'rxjs';
import { TaskVm } from '../view-models/task-vm';
import { AddTaskComponent } from './add-task/add-task.component';
import { TasksService } from '../services/tasks.service';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-tasks-list',
  templateUrl: './tasks-list.component.html',
})
export class TasksListComponent implements OnInit {
  constructor(
    private dataService: TasksService,
    private dialog: MatDialog,
    private route: ActivatedRoute,
    private notificationService: NotificationService,
  ) {
  }

  projectId: number = 0;
  items: TaskVm[] = [];
  isLoading: boolean = true;
  displayedColumns: string[] = ['title', 'description', 'deadline', 'project', 'assignee'];

  ngOnInit(): void {
    this.route.paramMap.pipe(
      tap((params: ParamMap) => {
        this.projectId = +params.get('id')!;
      }),
      switchMap(_ => this.getData(this.projectId))
    ).subscribe();
  }

  private getData(projectId: number) {
    return this.dataService.getByProjectId(projectId).pipe(
      tap((x: TaskVm[]) => {
        this.items = x;
        this.isLoading = false;
      })
    );
  }

  readonly addDialog = inject(MatDialog);    

  addTask() {
    const dialogRef = this.dialog.open(AddTaskComponent, { data: {projectId: this.projectId} });

    dialogRef.afterClosed().pipe(
      filter(data => data),
      switchMap(data => this.dataService.create(data)),
      tap(_ => this.notificationService.showMessage('Added')),
      switchMap(_ => this.getData(this.projectId))
    ).subscribe();
  }

  editTask(id: number) {
    const dialogRef = this.dialog.open(AddTaskComponent, { data: { taskId: id } });

    dialogRef.afterClosed().pipe(
      filter(data => data),
      switchMap(data => this.dataService.update(data)),
      tap(_ => this.notificationService.showMessage('Updated')),
      switchMap(_ => this.getData(this.projectId))
    ).subscribe();
  }
}
