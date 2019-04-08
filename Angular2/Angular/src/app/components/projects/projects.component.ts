import { Component, OnInit } from '@angular/core';
import { ProjectsService } from 'src/app/services/projects.service';
import { Router } from '@angular/router';
import { Project } from 'src/app/models/project';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  public projectList: Project[];
  public newProjectName: string;
  public newProjectTag: string;

  constructor(private service: ProjectsService, private router: Router, private accountService: AccountService) {
  }

  ngOnInit() {
    this.GetAllProjects();
  }

  public createProject() {
    this.service.createProject(this.newProjectName, this.newProjectTag)
      .subscribe(
      createdProject => {
        this.GetAllProjects();
      })
  }

  public deleteProject(projectId: number) {
    this.service.deleteProject(projectId)
      .subscribe(() => {
        this.projectList = this.projectList.filter(project => project.Id !== projectId)
      })
  }  

  public GetAllProjects() {
    this.service.getAllProjects().subscribe(
      projectList => {
        this.projectList = projectList;
      }
    );
  }

  public GetCurrentUserProjects() {
    console.log(this.accountService.getCurrentUser().UserName);
    this.service.GetCurrentUserProjects(this.accountService.getCurrentUser()).subscribe(
      projectList => {
        this.projectList = projectList;
      }
    );
  }
}
