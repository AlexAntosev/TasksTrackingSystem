import { Component, OnInit } from '@angular/core';
import { ProjectsService } from 'src/app/services/projects.service';
import { ActivatedRoute } from '@angular/router';
import { Project } from 'src/app/models/project';
import { Task } from 'src/app/models/task';
import { AccountService } from 'src/app/services/account.service';
import { CurrentUserInitializerService } from 'src/app/services/current-user-initializer.service';
import { UserWithRole } from 'src/app/models/user-with-role';
import { TokenService } from 'src/app/services/token.service';
import { ProjectEditComponent } from 'src/app/components/projects/project-edit/project-edit.component';
import { UsersService } from 'src/app/services/users.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Role } from 'src/app/models/role.enum';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.css']
})
export class ProjectDetailsComponent implements OnInit {

  selectedProject: Project;
  currentTask: Task;
  isAvailable: boolean = false;

  constructor(private service: ProjectsService,
    private route: ActivatedRoute,
    private router: Router,
    private userInitializeService: CurrentUserInitializerService,
    private tokenService: TokenService,
    private userService: UsersService,
    private modalService: NgbModal,
    private accountService: AccountService) {

  }

  ngOnInit() {
    this.selectedProject = this.route.snapshot.data['project'];
    this.userInitializeService.currentProjectId = this.selectedProject.Id;

    this.userInitializeService.loadCurrentUser().then(() => {
        this.isAvailable = true;
      });
  }

  public onChooseTaskEvent(task: Task) {
    this.currentTask = task;
  }

  public openEditModal(project: Project) {
    const editProject = {
      Name: project.Name,
      Tag: project.Tag,
      Url: project.Url
    }
    const modalRef = this.modalService.open(ProjectEditComponent);
    modalRef.componentInstance.project = editProject;

    modalRef.componentInstance.saveEntry
      .subscribe(p => {
        this.service.editProject(project.Id, p).subscribe();
      });
  }

  public deleteProject(projectId: number): void {
    this.service.deleteProject(projectId)
      .subscribe(() => {
        this.router.navigate(['/home']);
      });
  }
}
