import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/core/services/project.service';
import { Project } from 'src/app/core/models/project.model';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {

  constructor(private service : ProjectService) { }

  ngOnInit() {
    this.service.refreshProjectList();
  }

  populateForm(project : Project){
    this.service.formData = Object.assign({}, project);
  }

  onDelete(projectId : number){
    this.service.deleteProject(projectId).subscribe(res => {
      this.service.refreshProjectList();
    })
  }

}
