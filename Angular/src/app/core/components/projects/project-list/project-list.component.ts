import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/core/services/project.service';
import { Project } from 'src/app/core/models/project.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {

  projects: Project[];
  
  constructor(private service : ProjectService, private router: Router) { }

  ngOnInit() {
    this.getProjects();
  }

  getProjects() {
    this.service.getProjects();
  }

  onSelect(project: Project){
    this.service.getProject(project.Id);
    this.router.navigate(['/projects/details/'+project.Id, {proj: project}]);
  }

  onUpdate(project: Project) {
    this.router.navigate(['/projects/'+project.Id, {id: project.Id}]);
  }

  onDelete(projectId: number) {
    //if(this.service.formData.Tasks.length = 0)    
    this.service.deleteProject(projectId).subscribe(res => {
      this.service.getProjects();
    })
  }

}
