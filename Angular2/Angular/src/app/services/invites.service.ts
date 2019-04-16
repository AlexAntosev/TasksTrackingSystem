import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Invite } from 'src/app/models/invite';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ErrorService } from 'src/app/services/error.service';
import { UsersService } from 'src/app/services/users.service';
import { User } from 'src/app/models/user';

@Injectable({
  providedIn: 'root'
})
export class InvitesService {
  public url: string = "http://localhost:60542/api";

  constructor(private http: HttpClient, private errorService: ErrorService) { }

  public sendInvite(invite: Invite): Observable<Invite> {
    return this.http.post<Invite>(this.url + '/Invites', invite)
      .pipe(
      catchError(this.errorService.handleError)
      );
  }

  public deleteInvite(id: number): Observable<Invite> {
    return this.http.delete<Invite>(this.url + '/Invites/' + id)
      .pipe(
      catchError(this.errorService.handleError)
      );
  }

  public getInviteById(id: number): Observable<Invite> {
    return this.http.get<Invite>(this.url + '/Invites/' + id)
      .pipe(
      catchError(this.errorService.handleError)
      );
  }

  public getAllInvitesByProjectId(projectId: number): Observable<Invite[]> {
    return this.http.get<Invite[]>(this.url + '/Projects/' + projectId + '/Invites')
      .pipe(
      catchError(this.errorService.handleError)
      );
  }

  public getAllInvitesByReceiverId(userId: number): Observable<Invite[]> {
    return this.http.get<Invite[]>(this.url + '/Users/' + userId + '/Invites')
      .pipe(
      catchError(this.errorService.handleError)
      );
  }
}
