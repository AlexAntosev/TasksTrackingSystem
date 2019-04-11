import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from 'src/app/models/task';
import { HttpParams } from '@angular/common/http';
import { Priority } from 'src/app/models/priority.enum';
import { AccountService } from 'src/app/services/account.service';

@Injectable({
  providedIn: 'root'
})
export class TasksService {

  public url: string = "http://localhost:60542/api";
  
  constructor(private http: HttpClient, private accountService: AccountService) { }

  public getTasksByProjectId(projectId : number): Observable<Task[]>  {
    return this.http.get<Task[]>(this.url + '/Projects/' + projectId + '/Tasks');
  }

  public getTask(taskId : number): Observable<Task>  {
    return this.http.get<Task>(this.url + '/Tasks/' + taskId);
  }

  public createTask(taskName: string, taskDescription: string, taskPriority: Priority, projectId: number, executorUserId: number): Observable<Task>{
    let projectIdParam = new HttpParams().set('projectId', ''+projectId).set('creatorUserId', ''+this.accountService.getCurrentUser().Id).set('executorUserId', ''+executorUserId);
    return this.http.post<Task>(this.url + '/Tasks', {Name: taskName, Description: taskDescription, Priority: taskPriority}, {params: projectIdParam});
  }

  public deleteTask(taskId: number): Observable<Task>  {
    return this.http.delete<Task>(this.url + '/Tasks/' + taskId)
  }

  public updateTask(formData: Task): Observable<Task>  {
    return this.http.put<Task>(this.url + '/Tasks/' + formData.Id, formData);
  }  
}
