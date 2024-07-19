import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectsService } from '../services/projects.service';
import { ProjectVm } from '../view-models/project-vm';
import { MatDialog } from '@angular/material/dialog';
import { AddProjectComponent } from './add-project/add-project.component';
import { filter, switchMap, tap } from 'rxjs';
import { AddTaskComponent } from '../tasks-list/add-task/add-task.component';
import { TasksService } from '../services/tasks.service';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-projects-list',
  templateUrl: './projects-list.component.html'
})
export class ProjectsListComponent implements OnInit {
  constructor(
    private dataService: ProjectsService,
    private taskService: TasksService,
    private notificationService: NotificationService,
    private dialog: MatDialog,
    private router: Router,
    private route: ActivatedRoute) {
  }

  items: ProjectVm[] = [];
  isLoading: boolean = true;
  displayedColumns: string[] = ['name'];

  ngOnInit(): void {
    this.getData().subscribe();
  }

  private getData() {
    this.isLoading = true;
    return this.dataService.getData().pipe(
      tap((x: ProjectVm[]) => {
        this.items = x;
        this.isLoading = false;
      })
    );
  }

  readonly addDialog = inject(MatDialog);
    

  addProject() {
    const dialogRef = this.dialog.open(AddProjectComponent);

    dialogRef.afterClosed().pipe(
      filter(data => data),
      switchMap(data => this.dataService.create(data)),
      tap(_ => this.notificationService.showMessage('Added')),
      switchMap(_ => this.getData()),
    ).subscribe();
  }

  addTask() {
    const dialogRef = this.dialog.open(AddTaskComponent);

    dialogRef.afterClosed().pipe(
      filter(data => data),
      switchMap(data => this.taskService.create(data)),
      tap(_ => this.notificationService.showMessage('Added'))
    ).subscribe();
  }

  openProject(id: number) {
    this.router.navigate([id], { relativeTo: this.route });
  }
}
