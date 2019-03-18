import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import {MatSelectModule, } from '@angular/material/select';
import {MatInputModule, } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import { AppComponent } from './app.component';
import { ProjectsComponent } from './core/components/projects/projects.component';

import { ProjectService } from 'src/app/core/services/project.service';
import { TaskService } from 'src/app/core/services/task.service';
import { TasksComponent } from 'src/app/core/components/tasks/tasks.component';
import { AccountComponent } from './core/components/account/account.component';
import { AccountService } from 'src/app/core/services/account.service';



@NgModule({
  declarations: [
    AppComponent,
    ProjectsComponent,
    TasksComponent,
    AccountComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    MatSelectModule,
    MatInputModule,
    BrowserAnimationsModule
  ],
  providers: [ProjectService, TaskService, AccountService],
  bootstrap: [AppComponent]
})
export class AppModule { }
