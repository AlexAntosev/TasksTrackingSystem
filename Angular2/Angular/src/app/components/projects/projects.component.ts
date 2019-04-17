import { Component, OnInit } from '@angular/core';
import { ProjectsService } from 'src/app/services/projects.service';
import { Router } from '@angular/router';
import { Project } from 'src/app/models/project';
import { AccountService } from 'src/app/services/account.service';
import { ProjectEditComponent } from 'src/app/components/projects/project-edit/project-edit.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UsersService } from 'src/app/services/users.service';
import { Role } from 'src/app/models/role.enum';
import { CurrentUserInitializerService } from 'src/app/services/current-user-initializer.service';

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
    private modalService: NgbModal,
    private userService: UsersService,
    private userInitializeService: CurrentUserInitializerService) {
  }

  ngOnInit() {
    this.getCurrentUserProjects();
  }

  public getAllProjects(): void {
    this.service.getAllProjects().subscribe(projectList => this.projectList = projectList);
  }

  public getCurrentUserProjects(): void {
    this.service.GetCurrentUserProjects(this.accountService.getCurrentUserWithRole().User)
      .subscribe(projectList => this.projectList = projectList);
  }  

  public openCreateModal() {
    const newProject = {
      Name: '',
      Tag: '',
      Url: ''
    }
    const modalRef = this.modalService.open(ProjectEditComponent);
    modalRef.componentInstance.project = newProject;

    modalRef.componentInstance.saveEntry
      .subscribe(p => {
        debugger;
        this.service.createProject(p).subscribe(createdProject => {          
          this.userService.addUserToProject(createdProject.Id, this.accountService.getCurrentUserWithRole().User.Id, Role.Admin).subscribe(() => this.getCurrentUserProjects());
          
        });
      });
  }
}
