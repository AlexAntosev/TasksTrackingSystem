import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TasksComponent } from 'src/app/core/components/tasks/tasks.component';
import { TaskCreateComponent } from 'src/app/core/components/tasks/task-create/task-create.component';
import { TaskDetailsComponent } from 'src/app/core/components/tasks/task-details/task-details.component';
import { TaskListComponent } from 'src/app/core/components/tasks/task-list/task-list.component';
import { TasksRoutingModule } from 'src/app/core/components/tasks/tasks-routing/tasks-routing.module';
import { MatSelectModule, MatInputModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    TasksComponent,
    TaskCreateComponent,
    TaskDetailsComponent,
    TaskListComponent
  ],
  imports: [
    CommonModule,
    FormsModule,      
    MatSelectModule,
    MatInputModule,
    BrowserAnimationsModule,
    TasksRoutingModule,
  ],

  exports: [
    TasksComponent
  ]
})
export class TasksModule { }
