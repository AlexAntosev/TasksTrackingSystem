import { Injectable } from '@angular/core';
import { Project } from 'src/app/core/models/project.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {

  formData : Project;
  list : Project[];
  readonly rootURL = "http://localhost:60708/api";

  constructor(private http : HttpClient) { }

  createProject(formData : Project){
    return this.http.post(this.rootURL+'/Project/Create', formData);
  }

  refreshProjectList(){
    this.http.get(this.rootURL+'/Project/Get')
    .toPromise().then(res => this.list = res as Project[]);
  }

  updateProject(formData : Project){
    return this.http.put(this.rootURL+'/Project/Update/' + formData.Id, formData);
  }

  deleteProject(projectId : number){
    return this.http.delete(this.rootURL+'/Project/Delete/'+projectId)
  }
}
