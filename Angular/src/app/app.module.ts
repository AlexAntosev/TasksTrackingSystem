import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { ProjectsComponent } from './core/components/projects/projects.component';
import { ProjectComponent } from './core/components/projects/project/project.component';
import { ProjectListComponent } from './core/components/projects/project-list/project-list.component';
import { ProjectService } from 'src/app/core/services/project.service';
import { TasksComponent } from './core/components/tasks/tasks.component';
import { TaskComponent } from './core/components/tasks/task/task.component';
import { TaskListComponent } from './core/components/tasks/task-list/task-list.component';
import { ProjectHeaderComponent } from './core/components/projects/project-header/project-header.component';
import { TaskHeaderComponent } from './core/components/tasks/task-header/task-header.component';
import { ProjectInfoComponent } from './core/components/projects/project-info/project-info.component';
import { TaskService } from 'src/app/core/services/task.service';

@NgModule({
  declarations: [
    AppComponent,
    ProjectsComponent,
    ProjectComponent,
    ProjectListComponent,
    TasksComponent,
    TaskComponent,
    TaskListComponent,
    ProjectHeaderComponent,
    TaskHeaderComponent,
    ProjectInfoComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [ProjectService, TaskService],
  bootstrap: [AppComponent]
})
export class AppModule { }
