import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user';
import { ErrorService } from 'src/app/services/error.service';
import { catchError } from 'rxjs/internal/operators/catchError';
import { Invite } from 'src/app/models/invite';

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

  public getUsersByProjectId(projectId: number): Observable<User[]> {
    return this.http.get<User[]>(this.url + '/Projects/' + projectId + '/Users')
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
    const projectId = ''+invite.ProjectId;
    const receiverId = ''+invite.ReceiverId;
    return this.http.put<User>(this.url + '/Projects/' + invite.ProjectId + '/Users', {}, { params: { projectId: projectId, userId: receiverId} })
      .pipe(
        catchError(this.errorService.handleError)
      );
  }

  public addUserToProject(projectId: number, userId: number): Observable<User> {
    return this.http.put<User>(this.url + '/Projects/' + projectId + '/Users', {}, { params: { projectId: ''+projectId, userId: ''+userId} })
      .pipe(
        catchError(this.errorService.handleError)
      );
  }
}
