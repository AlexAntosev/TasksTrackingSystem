import { Injectable } from '@angular/core';
import { Task } from 'src/app/core/models/task.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Project } from 'src/app/core/models/project.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  formData: Task;
  list: Task[];
  readonly rootURL = "http://localhost:60542/api";  
  projectId : number;

  constructor(private http: HttpClient) { }

  createTask(formData: Task, projectId: number) {
    let projectIdParam = new HttpParams().set('projectId', ''+projectId);
    return this.http.post(this.rootURL + '/Tasks', formData, {params: projectIdParam});
  }

  updateTask(formData: Task) {
    return this.http.put(this.rootURL + '/Tasks/' + formData.Id, formData);
  }

  deleteTask(id: number) {
    return this.http.delete(this.rootURL + '/Tasks/' + id);
  }

  getByProject(projectId : number) {
    return this.http.get<Task[]>(this.rootURL + '/Projects/' + projectId + '/Tasks').subscribe(data => this.list = data);
  }
}
