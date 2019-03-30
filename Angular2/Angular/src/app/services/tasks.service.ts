import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from 'src/app/models/task';
import { HttpParams } from '@angular/common/http';
import { Priority } from 'src/app/models/priority.enum';

@Injectable({
  providedIn: 'root'
})
export class TasksService {

  public url: string = "http://localhost:60542/api";
  
  constructor(private http: HttpClient) { }

  getTasksByProjectId(projectId : number): Observable<Task[]>  {
    return this.http.get<Task[]>(this.url + '/Projects/' + projectId + '/Tasks');
  }

  public createTask(taskName: string, taskDescription: string, taskPriority: Priority, projectId: number): Observable<Task>{
    let projectIdParam = new HttpParams().set('projectId', ''+projectId);
    return this.http.post<Task>(this.url + '/Tasks', {Name: taskName, Description: taskDescription, Priority: taskPriority}, {params: projectIdParam});
  }

  public deleteTask(taskId: number): Observable<any> {
    return this.http.delete(this.url + '/Tasks/' + taskId)
  }

  updateTask(formData: Task) {
    return this.http.put(this.url + '/Tasks/' + formData.Id, formData);
  }  
}
