import { Injectable } from '@angular/core';
import { Project } from 'src/app/core/models/project.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  formData: Project;
  list: Project[];
  readonly rootURL = "http://localhost:60708/api/Project";

  constructor(private http: HttpClient) { }

  createProject(formData: Project) {
    return this.http.post(this.rootURL + '/Create', formData);
  }

  getProjects() {
    this.http.get(this.rootURL + '/Get')
      .toPromise().then(res => this.list = res as Project[]);
  }

  updateProject(formData: Project) {
    return this.http.put(this.rootURL + '/Update/' + formData.Id, formData);
  }

  deleteProject(projectId: number) {
    return this.http.delete(this.rootURL + '/Delete/' + projectId)
  }

  getProject(projectId: number) {
    this.http.get(this.rootURL + '/Get/' + projectId)
      .toPromise().then(res => this.formData = res as Project);
  }
}
