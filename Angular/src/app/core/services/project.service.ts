import { Injectable } from '@angular/core';
import { Project } from 'src/app/core/models/project.model';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http/src/headers';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  formData: Project;
  list: Project[];
  readonly rootURL = "http://localhost:60542/api/Projects";
  tokenKey: string = "tokenInfo";

  constructor(private http: HttpClient) { 
  }

  createProject(formData: Project) {
    return this.http.post(this.rootURL, formData);
  }

  getProjects() {
    return this.http.get<Project[]>(this.rootURL).subscribe(data => this.list = data);
  }

  updateProject(formData: Project) {
    var token = sessionStorage.getItem(this.tokenKey);
    var myHeaders : any = {};
    if (token) {
        myHeaders.Authorization = 'Bearer ' + token;
    }
    return this.http.put(this.rootURL + '/' + formData.Id, formData, {headers: myHeaders});
  }

  deleteProject(projectId: number) {
    return this.http.delete(this.rootURL + '/' + projectId)
  }

  getProject(projectId: number) {
    return this.http.get<Project>(this.rootURL + '/' + projectId).subscribe(data => this.formData = data);
  }
}
