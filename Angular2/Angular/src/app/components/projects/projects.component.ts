import { Component, OnInit } from '@angular/core';
import { ProjectsService } from 'src/app/services/projects.service';
import { Router } from '@angular/router';
import { Project } from 'src/app/models/project';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  public projectList: Project[];
  public newProjectName: string;
  public newProjectTag: string;

  constructor(private service: ProjectsService, private router: Router) {
  }

  ngOnInit() {
    this.service.getProjects().subscribe(
      projectList => {
        this.projectList = projectList;
      }
    );
  }

  public createProject() {
    this.service.createProject(this.newProjectName, this.newProjectTag)
      .subscribe(
      createdProject => {
        const project = <Project>{
          Id: createdProject.Id,
          Name: createdProject.Name,
          Tag: createdProject.Tag
        };
        this.projectList.push(project);
      })
  }

  public deleteProject(projectId: number) {
    this.service.deleteProject(projectId)
      .subscribe(() => {
        this.projectList = this.projectList.filter(project => project.Id !== projectId)
      })
  }  
}
