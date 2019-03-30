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

  public getUsersByProjectId(projectId : number): Observable<User[]>  {
    return this.http.get<User[]>(this.url + '/Projects/' + projectId + '/Users');
  }

  public addUserToProject(userId: number, projectId: number): Observable<any>{
    return this.http.put(this.url + '/Projects/' + projectId + '/Users', {},{params:{projectId: projectId, userId: userId}});
  }

  public removeUserFromProject(userId: number, projectId: number): Observable<any>{
    return this.http.delete(this.url + '/Projects/' + projectId + '/Users', {params:{projectId: projectId, userId: userId}});
  }
}
