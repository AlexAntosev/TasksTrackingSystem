import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  public url: string = "http://localhost:60542/api";
  
  constructor(private http: HttpClient) { }

  public getAllUsers(): Observable<User[]>  {
    return this.http.get<User[]>(this.url + '/Users');
  }

  public getUsersByProjectId(projectId : number): Observable<User[]>  {
    return this.http.get<User[]>(this.url + '/Projects/' + projectId + '/Users');
  }

  public addUserToProject(userId, projectId): Observable<User>{
    return this.http.put<User>(this.url + '/Projects/' + projectId + '/Users', {},{params:{projectId: projectId, userId: userId}});
  }

  public removeUserFromProject(userId, projectId): Observable<User>{
    return this.http.delete<User>(this.url + '/Projects/' + projectId + '/Users', {params:{projectId: projectId, userId: userId}});
  } 

  public getUserById(userId: number): Observable<User>{
    return this.http.get<User>(this.url + '/Users/' + userId);
  }
}