import { Injectable } from '@angular/core';
import { Project } from 'src/app/models/project';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { User } from 'src/app/models/user';
import { catchError } from 'rxjs/internal/operators/catchError';
import { ErrorService } from 'src/app/services/error.service';

@Injectable()
export class ProjectsService {

  public url: string = "http://localhost:60542/api/Projects";

  constructor(private http: HttpClient, private errorService: ErrorService) { }

  public getAllProjects(): Observable<Project[]> {
    return this.http.get<Project[]>(this.url)
      .pipe(
      catchError(this.errorService.handleError)
      );
  }

  public createProject(newProject: Project): Observable<Project> {
    return this.http.post<Project>(this.url, { Name: newProject.Name, Tag: newProject.Tag, Url: newProject.Url })
      .pipe(
      catchError(this.errorService.handleError)
      );
  }

  public deleteProject(projectId: number): Observable<any> {
    return this.http.delete(this.url + '/' + projectId)
      .pipe(
      catchError(this.errorService.handleError)
      );
  }

  public getProject(projectId: number): Observable<Project> {
    return this.http.get<Project>(this.url + '/' + projectId)
      .pipe(
      catchError(this.errorService.handleError)
      );
  }

  public editProject(projectId: number, editedProject: Project): Observable<Project> {
    return this.http.put<Project>(this.url + '/' + projectId, { Name: editedProject.Name, Tag: editedProject.Tag, Url: editedProject.Url })
    .pipe(
      catchError(this.errorService.handleError)
    );
  }

  public GetCurrentUserProjects(user: User): Observable<Project[]> {
    return this.http.get<Project[]>('http://localhost:60542/api/Users/' + user.UserName + '/Projects')
      .pipe(
      catchError(this.errorService.handleError)
      );
  }

}
