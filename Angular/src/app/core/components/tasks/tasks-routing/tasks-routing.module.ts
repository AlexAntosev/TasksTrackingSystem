import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListComponent } from 'src/app/core/components/tasks/task-list/task-list.component';
import { TaskDetailsComponent } from 'src/app/core/components/tasks/task-details/task-details.component';
import { TaskCreateComponent } from 'src/app/core/components/tasks/task-create/task-create.component';
import { Routes } from '@angular/router';
import { RouterModule } from '@angular/router';

const tasksRoutes: Routes = [
  { path: 'tasks',  component: TaskListComponent },
  { path: 'tasks/:id', component: TaskDetailsComponent},
  { path: 'tasks/create',  component: TaskCreateComponent}
];


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(tasksRoutes)
  ], 

  exports: [
    RouterModule
  ]
})
export class TasksRoutingModule { }
