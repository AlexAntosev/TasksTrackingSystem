import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from 'src/app/models/task';
import { HttpParams } from '@angular/common/http';
import { Priority } from 'src/app/models/priority.enum';
import { AccountService } from 'src/app/services/account.service';
import { catchError } from 'rxjs/internal/operators/catchError';
import { ErrorService } from 'src/app/services/error.service';
import { Type } from 'src/app/models/type.enum';
import { Status } from 'src/app/models/status.enum';

@Injectable({
  providedIn: 'root'
})
export class TasksService {

  public url: string = "http://localhost:60542/api";

  constructor(private http: HttpClient, private accountService: AccountService, private errorService: ErrorService) { }

  public getTasksByProjectId(projectId: number): Observable<Task[]> {
    return this.http.get<Task[]>(this.url + '/Projects/' + projectId + '/Tasks').pipe(
      catchError(this.errorService.handleError)
    );
  }

  public getTask(taskId: number): Observable<Task> {
    return this.http.get<Task>(this.url + '/Tasks/' + taskId)
    .pipe(
      catchError(this.errorService.handleError)
    );
  }

  public createTask(task: Task): Observable<Task> {
    return this.http.post<Task>(this.url + '/Tasks', task)
    .pipe(
      catchError(this.errorService.handleError)
    );
  }

  public deleteTask(taskId: number): Observable<Task> {
    return this.http.delete<Task>(this.url + '/Tasks/' + taskId)
    .pipe(
      catchError(this.errorService.handleError)
    );
  }

  public updateTask(formData: Task): Observable<Task> {
    return this.http.put<Task>(this.url + '/Tasks/' + formData.Id, formData)
    .pipe(
      catchError(this.errorService.handleError)
    );
  }
}
