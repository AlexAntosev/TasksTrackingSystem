import { Component, OnInit } from '@angular/core';
import { ProjectsService } from 'src/app/services/projects.service';
import { Router } from '@angular/router';
import { Project } from 'src/app/models/project';
import { AccountService } from 'src/app/services/account.service';
import { ProjectEditComponent } from 'src/app/components/projects/project-edit/project-edit.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UsersService } from 'src/app/services/users.service';
import { Role } from 'src/app/models/role.enum';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  public projectList: Project[];

  constructor(private service: ProjectsService,
    private router: Router,
    private accountService: AccountService,
    private modalServie: NgbModal,
    private userService: UsersService) {
  }

  ngOnInit() {
    this.GetAllProjects();
  }

  public deleteProject(projectId: number) {
    this.service.deleteProject(projectId)
      .subscribe(() => {
        this.projectList = this.projectList.filter(project => project.Id !== projectId)
      });
  }

  public GetAllProjects() {
    this.service.getAllProjects().subscribe(
      projectList => {
        this.projectList = projectList;
      }
    );
  }

  public GetCurrentUserProjects() {
    this.service.GetCurrentUserProjects(this.accountService.getCurrentUser()).subscribe(
      projectList => {
        this.projectList = projectList;
      }
    );
  }

  public openEditModal(project: Project) {

    let newProject = {
      Name: project.Name,
      Tag: project.Tag,
      Url: project.Url
    }

    const modalRef = this.modalServie.open(ProjectEditComponent);
    modalRef.componentInstance.project = newProject;
    modalRef.componentInstance.saveEntry
      .subscribe(
      (p) => {
        this.service.editProject(project.Id, p).subscribe(
          () => { this.GetAllProjects() }
        );

      }
      );
  }

  public openCreateModal() {

    let newProject = {
      Name: '',
      Tag: '',
      Url: ''
    }

    const modalRef = this.modalServie.open(ProjectEditComponent);
    modalRef.componentInstance.project = newProject;
    modalRef.componentInstance.saveEntry
      .subscribe(
      (p) => {
        this.service.createProject(p).subscribe(
          createdProject => {
            this.GetAllProjects();
            this.userService.addUserToProject(createdProject.Id, this.accountService.getCurrentUser().Id, Role.Admin).subscribe();
          }
        );
      }
      );
  }
}
