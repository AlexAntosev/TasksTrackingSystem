import { Injectable } from '@angular/core';
import { Project } from 'src/app/core/models/project.model';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http/src/headers';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  formData: Project;
  list: Project[];
  readonly rootURL = "http://localhost:60542/api/Project";
  tokenKey: string = "tokenInfo";

  constructor(private http: HttpClient) { 
  }

  createProject(formData: Project) {
    return this.http.post(this.rootURL + '/Create', formData);
  }

  getProjects() {
    this.http.get(this.rootURL + '/Get')
      .toPromise().then(res => this.list = res as Project[]);
  }

  updateProject(formData: Project) {
    var token = sessionStorage.getItem(this.tokenKey);
    var myHeaders : any = {};
    if (token) {
        myHeaders.Authorization = 'Bearer ' + token;
    }
    return this.http.put(this.rootURL + '/Update/' + formData.Id, formData, {headers: myHeaders});
  }

  deleteProject(projectId: number) {
    return this.http.delete(this.rootURL + '/Delete/' + projectId)
  }

  getProject(projectId: number) {
    this.http.get(this.rootURL + '/Get/' + projectId)
      .toPromise().then(res => this.formData = res as Project);
  }
}
