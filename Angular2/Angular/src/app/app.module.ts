import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatCheckboxModule, MatToolbarModule, MatCardModule, MatDialogModule, MatSelectModule, MatGridListModule, MatSnackBarModule, MatDatepickerModule, MatNativeDateModule, MatInputModule } from '@angular/material';

import { AppRoutingModule, routes } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { ProjectsComponent } from './components/projects/projects.component';
import { ProjectsService } from 'src/app/services/projects.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { CommentsComponent } from './components/comments/comments.component';
import { CommentsService } from 'src/app/services/comments.service';
import { ErrorService } from 'src/app/services/error.service';
import { ServerErrorInterceptor } from 'src/app/core/interceptors/server-error-interceptor';
import { ProjectEditComponent } from 'src/app/components/projects/project-edit/project-edit.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SearchingFilterPipe } from 'src/app/core/pipes/searching-filter.pipe';
import { InvitesComponent } from './components/invites/invites.component';
import { AlertComponent } from './components/alert/alert.component';
import { AlertService } from 'src/app/services/alert.service';
import { TaskEditComponent } from './components/tasks/task-edit/task-edit.component';
import { HomeComponent } from './components/home/home.component';
import { AdminModule } from 'src/app/components/admin/admin.module';
import { AdminGuard } from 'src/app/core/guard/admin.guard';

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
    UsersComponent,
    CommentsComponent,
    ProjectEditComponent,
    SearchingFilterPipe,
    InvitesComponent,
    AlertComponent,
    TaskEditComponent,
    HomeComponent
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
    MatSnackBarModule,
    MatGridListModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
    HttpClientModule,
    ReactiveFormsModule,
    AdminModule,
    NgbModule,
    AppRoutingModule
  ],
  providers: [
    ProjectsService,
    ProjectDetailsResolver,
    TasksService,
    TaskDetailsResolver,
    AccountService,
    UsersService,
    CommentsService,
    TokenService,
    CurrentUserInitializerService,
    ErrorService,
    AdminGuard,
    AlertService,
    { provide: Window, useValue: window },
    {
       provide: HTTP_INTERCEPTORS,
       useClass: TokenInterceptor,
       multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ServerErrorInterceptor,
      multi: true
    },
    { 
      provide: APP_INITIALIZER, 
      useFactory: loadCurrentUser, 
      deps: [CurrentUserInitializerService], 
      multi: true 
    } 
  ],
  bootstrap: [AppComponent],

  entryComponents: [ProjectEditComponent, TaskEditComponent]
})
export class AppModule { }

function loadCurrentUser(currentUserService: CurrentUserInitializerService): () => Promise<boolean> {
  return () => currentUserService.loadCurrentUser();
}
