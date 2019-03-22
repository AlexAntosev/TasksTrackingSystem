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
  readonly rootURL = "http://localhost:60542/api";  

  constructor(private http: HttpClient) { }

  createTask(formData: Task, projectId: number) {
    let projectIdParam = new HttpParams().set('projectId', ''+projectId);
    return this.http.post(this.rootURL + '/Task/Create', formData, {params: projectIdParam});
  }

  updateTask(formData: Task) {
    return this.http.put(this.rootURL + '/Task/Update/' + formData.Id, formData);
  }

  deleteTask(id: number) {
    return this.http.delete(this.rootURL + '/Task/Delete/' + id);
  }

  refreshTaskList(projectId : number) {
    return this.http.get<Task[]>(this.rootURL + '/Project/' + projectId + '/Task/Get').subscribe(data => this.list = data);
  }

  getByProject(project: Project) {
    this.currentProject = project;
    this.list = this.currentProject.Tasks;
    //this.http.get(this.rootURL + '/Project/' + this.currentProject.Id + '/Task/Get')
    // .toPromise().then(res => this.list = res as Task[]);
    console.log(this.list);
  }
}
