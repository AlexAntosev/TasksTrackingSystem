import { Injectable } from '@angular/core';
import { Project } from 'src/app/models/project';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';

@Injectable()
export class ProjectsService {

  public url: string = "http://localhost:60542/api/Projects";
  
  constructor(private http: HttpClient) { }

  public getAllProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(this.url);
  }

  public createProject(newProjectName: string, newProjectTag: string): Observable<Project>{
    return this.http.post<Project>(this.url, { Name: newProjectName, Tag: newProjectTag });
  }

  public deleteProject(projectId: number): Observable<any> {
    return this.http.delete(this.url + '/' + projectId)
  }

  public getProject(projectId: number): Observable<Project> {
    return this.http.get<Project>(this.url + '/' + projectId);
  }

  public GetCurrentUserProjects(userName: string): Observable<Project[]> {
    return this.http.get<Project[]>('http://localhost:60542/api/Users/' + userName + '/Projects');
  }

}
