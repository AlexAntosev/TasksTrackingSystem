import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes } from '@angular/router';

import { RouterModule } from '@angular/router';
import { ProjectsComponent } from 'src/app/core/components/projects/projects.component';
import { ProjectListComponent } from 'src/app/core/components/projects/project-list/project-list.component';
import { ProjectDetailsComponent } from 'src/app/core/components/projects/project-details/project-details.component';
import { ProjectEditComponent } from 'src/app/core/components/projects/project-edit/project-edit.component';
import { TasksModule } from 'src/app/core/components/tasks/tasks.module';

const projectsRoutes: Routes = [
  {
    path: 'projects', component: ProjectsComponent, children: [
      { path: '', component: ProjectListComponent },
      { path: 'id', component: ProjectDetailsComponent, loadChildren: 'src/app/core/components/tasks/tasks.module#TasksModule' },
      { path: 'create', component: ProjectEditComponent, data: { isNewProject: true } },
      { path: 'edit/id', component: ProjectEditComponent, data: { isNewProject: false } }
    ]
  },

];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(
      projectsRoutes
    )
  ],

  exports: [
    RouterModule
  ]
})
export class ProjectsRoutingModule { }
