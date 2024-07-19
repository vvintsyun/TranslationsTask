import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ProjectsListComponent } from './projects-list/projects-list.component';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatIconModule } from '@angular/material/icon';
import { ProjectsService } from './services/projects.service';
import { AddProjectComponent } from './projects-list/add-project/add-project.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ErrorInterceptor } from './error-handling/error-interceptor';
import { AppErrorHandler } from './error-handling/app-error-handler';
import { AddTaskComponent } from './tasks-list/add-task/add-task.component';
import { TasksListComponent } from './tasks-list/tasks-list.component';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { TasksService } from './services/tasks.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ProjectsListComponent,
    TasksListComponent,
    AddProjectComponent,
    AddTaskComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'projects', pathMatch: 'full' },
      { path: 'projects', component: ProjectsListComponent },
      { path: 'projects/:id', component: TasksListComponent },
    ]),
    BrowserAnimationsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatSnackBarModule,
    MatDatepickerModule,
    MatMomentDateModule,
    MatIconModule,
  ],
  providers: [
    ProjectsService,
    TasksService,

    { provide: ErrorHandler, useClass: AppErrorHandler },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
