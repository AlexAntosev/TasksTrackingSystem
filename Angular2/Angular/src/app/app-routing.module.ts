import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProjectsComponent } from 'src/app/components/projects/projects.component';
import { ProjectDetailsComponent } from 'src/app/components/project-details/project-details.component';
import { ProjectDetailsResolver } from 'src/app/components/project-details/project-details.resolver';
import { ActivatedRouteSnapshot } from '@angular/router';
import { RouterStateSnapshot } from '@angular/router';
import { SignUpComponent } from 'src/app/components/account/sign-up/sign-up.component';
import { SignInComponent } from 'src/app/components/account/sign-in/sign-in.component';
import { TaskDetailsComponent } from 'src/app/components/task-details/task-details.component';
import { TaskDetailsResolver } from 'src/app/components/task-details/task-details.resolver';

export const routes: Routes = [
  { path: '',  redirectTo: '/projects', pathMatch: 'full'},
  { path: 'projects',  component: ProjectsComponent},
  { path: 'projects/:id', component: ProjectDetailsComponent, resolve: { project: ProjectDetailsResolver}},
  { path: 'tasks/:id', component: TaskDetailsComponent, resolve: { task: TaskDetailsResolver}},
  { path: 'sign-up', component: SignUpComponent},
  { path: 'sign-in', component: SignInComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
