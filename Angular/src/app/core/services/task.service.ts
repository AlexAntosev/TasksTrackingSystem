import { Injectable } from '@angular/core';
import { Task } from 'src/app/core/models/task.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Project } from 'src/app/core/models/project.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  currentProject: Project;
  formData: Task;
  list: Task[];
  readonly rootURL = "http://localhost:60708/api";

  constructor(private http: HttpClient) { }

  createTask(formData: Task, projectId: number) {
    console.log(formData);
    debugger;
    //this.currentProject.Tasks.push(formData);
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

  getByProject(project: Project) {
    this.currentProject = project;
    this.list = this.currentProject.Tasks;
    //this.http.get(this.rootURL + '/Project/' + this.currentProject.Id + '/Task/Get')
    // .toPromise().then(res => this.list = res as Task[]);
    console.log(this.list);
  }
}
