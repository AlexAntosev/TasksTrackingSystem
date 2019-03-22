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
import { ProjectListComponent } from './core/components/projects/project-list/project-list.component';
import { ProjectDetailsComponent } from './core/components/projects/project-details/project-details.component';
import { ProjectEditComponent } from './core/components/projects/project-edit/project-edit.component';
import { PageNotFoundComponent } from './core/components/page-not-found/page-not-found.component';

const appRoutes: Routes = [
  {
    path: 'projects', component: ProjectsComponent,
    children: [
      { path: '', component: ProjectListComponent },
      { path: 'create', component: ProjectEditComponent, data: { isNewProject: true } },
      { path: ':id', component: ProjectEditComponent, data: { isNewProject: false }  },
      { path: 'details/:id', component: ProjectDetailsComponent, }
    ]
  },
  { path: 'tasks', component: TasksComponent },
  //{ path: 'projects/:id',      component: HeroDetailComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '', redirectTo: '/projects', pathMatch: 'full' },
  //{ path: '**', component: PageNotFoundComponent }
];


@NgModule({
  declarations: [
    AppComponent,
    ProjectsComponent,
    TasksComponent,
    AccountComponent,
    LoginComponent,
    RegisterComponent,
    ProjectListComponent,
    ProjectDetailsComponent,
    ProjectEditComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    MatSelectModule,
    MatInputModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(
      appRoutes,
      { enableTracing: true } // <-- debugging purposes only
    )
  ],
  providers: [ProjectService, TaskService, AccountService],
  bootstrap: [AppComponent]
})
export class AppModule { }
