import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user';
import { ErrorService } from 'src/app/services/error.service';
import { catchError } from 'rxjs/internal/operators/catchError';
import { Invite } from 'src/app/models/invite';
import { UserWithRole } from 'src/app/models/user-with-role';
import { Role } from 'src/app/models/role.enum';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  public url: string = "http://localhost:60542/api";

  constructor(private http: HttpClient, private errorService: ErrorService) { }

  public getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.url + '/Users')
      .pipe(
        catchError(this.errorService.handleError)
      );
  }

  public getUsersWithRolesByProjectId(projectId: number): Observable<UserWithRole[]> {
    return this.http.get<UserWithRole[]>(this.url + '/Projects/' + projectId + '/Users')
      .pipe(
        catchError(this.errorService.handleError)
      );
  }

  public removeUserFromProject(userId, projectId): Observable<User> {
    return this.http.delete<User>(this.url + '/Projects/' + projectId + '/Users', { params: { projectId: projectId, userId: userId } })
      .pipe(
        catchError(this.errorService.handleError)
      );
  }

  public getUserById(userId: number): Observable<User> {
    return this.http.get<User>(this.url + '/Users/' + userId)
      .pipe(
        catchError(this.errorService.handleError)
      );
  }

  public addUserToProjectByInvite(invite: Invite): Observable<User> {
    debugger;
    const projectId = ''+invite.ProjectId;
    const receiverId = ''+invite.ReceiverId;
    const roleId = ''+invite.Role;
    return this.http.put<User>(this.url + '/Projects/' + invite.ProjectId + '/Users', {UserId: invite.ReceiverId, Role: roleId}, {params: {projectId: projectId}})
      .pipe(
        catchError(this.errorService.handleError)
      );
  }

  public addUserToProject(projectId: number, userId: number, role: Role): Observable<User> {
    return this.http.put<User>(this.url + '/Projects/' + projectId + '/Users', {UserId: userId, Role: role})
      .pipe(
        catchError(this.errorService.handleError)
      );
  }
}
