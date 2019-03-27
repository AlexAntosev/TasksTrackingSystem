import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProjectsRoutingModule } from 'src/app/core/components/projects/projects-routing/projects-routing.module';
import { ProjectsComponent } from 'src/app/core/components/projects/projects.component';
import { ProjectListComponent } from 'src/app/core/components/projects/project-list/project-list.component';
import { ProjectEditComponent } from 'src/app/core/components/projects/project-edit/project-edit.component';
import { ProjectDetailsComponent } from 'src/app/core/components/projects/project-details/project-details.component';
import { TasksModule } from 'src/app/core/components/tasks/tasks.module';

@NgModule({
  declarations: [
    ProjectsComponent,
    ProjectListComponent,
    ProjectEditComponent,
    ProjectDetailsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ProjectsRoutingModule,
  ]
})
export class ProjectsModule { }
