import { Injectable } from '@angular/core';
import { Task } from 'src/app/core/models/task.model';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  formData: Task;
  list: Task[];
  readonly rootURL = "http://localhost:60708/api";

  constructor(private http: HttpClient) { }

  createTask(formData: Task, projectId: number) {
    console.log(formData);
    let projectIdParam = new HttpParams().set('projectId', ''+projectId);
    return this.http.post(this.rootURL + '/Task/Create', formData, {params: projectIdParam});
  }

  updateTask(formData: Task) {
    return this.http.put(this.rootURL + '/Task/Update/' + formData.Id, formData);
  }

  deleteTask(id: number) {
    return this.http.delete(this.rootURL + '/Task/Delete/' + id);
  }

  refreshTaskList() {
    this.http.get(this.rootURL + '/Task/Get')
      .toPromise().then(res => this.list = res as Task[]);
  }

  getByProjectId(projectId: number) {
    this.http.get(this.rootURL + '/Project/' + projectId + '/Task/Get')
      .toPromise().then(res => this.list = res as Task[]);
    console.log(this.list);
  }
}
