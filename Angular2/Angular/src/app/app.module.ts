import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatCheckboxModule, MatToolbarModule, MatCardModule, MatDialogModule, MatSelectModule } from '@angular/material';

import { AppRoutingModule, routes } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { ProjectsComponent } from './components/projects/projects.component';
import { ProjectsService } from 'src/app/services/projects.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ProjectDetailsComponent } from './components/project-details/project-details.component';
import { ProjectDetailsResolver } from 'src/app/components/project-details/project-details.resolver';
import { RouterModule } from '@angular/router';
import { TasksComponent } from './components/tasks/tasks.component';
import { TaskDetailsComponent } from './components/task-details/task-details.component';
import { SignInComponent } from './components/account/sign-in/sign-in.component';
import { SignUpComponent } from './components/account/sign-up/sign-up.component';
import { TasksService } from 'src/app/services/tasks.service';
import { AccountService } from 'src/app/services/account.service';
import { UsersComponent } from './components/users/users.component';
import { UsersService } from 'src/app/services/users.service';
import { TokenService } from 'src/app/services/token.service';
import { CurrentUserInitializerService } from 'src/app/services/current-user-initializer.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from 'src/app/core/interceptors/token-interceptor';
import { APP_INITIALIZER } from '@angular/core';
import { TaskDetailsResolver } from 'src/app/components/task-details/task-details.resolver';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    ProjectsComponent,
    ProjectDetailsComponent,
    TasksComponent,
    TaskDetailsComponent,
    SignInComponent,
    SignUpComponent,
    UsersComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatCheckboxModule,
    MatToolbarModule,
    MatCardModule,
    MatDialogModule,
    MatSelectModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    ProjectsService,
    ProjectDetailsResolver,
    TasksService,
    TaskDetailsResolver,
    AccountService,
    UsersService,
    TokenService,
    CurrentUserInitializerService,
    { provide: Window, useValue: window },
    {
       provide: HTTP_INTERCEPTORS,
       useClass: TokenInterceptor,
       multi: true
    },
    { 
      provide: APP_INITIALIZER, 
      useFactory: loadCurrentUser, 
      deps: [CurrentUserInitializerService], 
      multi: true 
    } 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

function loadCurrentUser(currentUserService: CurrentUserInitializerService): () => Promise<boolean> {
  return () => currentUserService.loadCurrentUser();
}
