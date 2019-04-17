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
import { HomeComponent } from 'src/app/components/home/home.component';
import { AdminGuard } from 'src/app/core/guard/admin.guard';
import { UsersForAdminComponent } from 'src/app/components/admin/users/users.component';

export const routes: Routes = [
  { path: '',  redirectTo: '/sign-in', pathMatch: 'full'},
  { path: 'home',  component: HomeComponent},
  { path: 'projects/:id', component: ProjectDetailsComponent, resolve: { project: ProjectDetailsResolver}},
  { path: 'tasks/:id', component: TaskDetailsComponent, resolve: { task: TaskDetailsResolver}},
  { path: 'sign-up', component: SignUpComponent},
  { path: 'sign-in', component: SignInComponent},
  { path: 'admin', 
  canActivate: [ AdminGuard ],
  canActivateChild: [ AdminGuard ],    
  children: [
      { path: 'user-list', component: UsersForAdminComponent }
  ] 
}  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
