import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatSelectModule, } from '@angular/material/select';
import { MatInputModule, } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import { AppComponent } from './app.component';
import { ProjectsComponent } from './core/components/projects/projects.component';

import { ProjectService } from 'src/app/core/services/project.service';
import { TaskService } from 'src/app/core/services/task.service';
import { TasksComponent } from 'src/app/core/components/tasks/tasks.component';
import { AccountComponent } from './core/components/account/account.component';
import { AccountService } from 'src/app/core/services/account.service';
import { LoginComponent } from './core/components/account/login/login.component';
import { RegisterComponent } from './core/components/account/register/register.component';

import { RouterModule, Routes } from '@angular/router';
import { ProjectListComponent } from 'src/app/core/components/projects/project-list/project-list.component';
import { ProjectDetailsComponent } from 'src/app/core/components/projects/project-details/project-details.component';
import { ProjectEditComponent } from 'src/app/core/components/projects/project-edit/project-edit.component';
import { PageNotFoundComponent } from 'src/app/core/components/page-not-found/page-not-found.component';
import { TaskListComponent } from 'src/app/core/components/tasks/task-list/task-list.component';
import { TaskCreateComponent } from 'src/app/core/components/tasks/task-create/task-create.component';
import { TaskDetailsComponent } from 'src/app/core/components/tasks/task-details/task-details.component';
import { ProjectsModule } from 'src/app/core/components/projects/projects.module';
import { TasksModule } from 'src/app/core/components/tasks/tasks.module';
import { AccountModule } from 'src/app/core/components/account/account.module';
import { AppRoutingModule } from 'src/app/app-routing/app-routing.module';




@NgModule({
  declarations: [
    AppComponent,
    PageNotFoundComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    MatSelectModule,
    MatInputModule,
    BrowserAnimationsModule,
    ProjectsModule,
    TasksModule,
    AccountModule,
    AppRoutingModule
  ],
  providers: [ProjectService, TaskService, AccountService],
  bootstrap: [AppComponent]
})
export class AppModule { }
