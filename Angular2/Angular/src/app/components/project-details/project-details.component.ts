import { Component, OnInit } from '@angular/core';
import { ProjectsService } from 'src/app/services/projects.service';
import { ActivatedRoute } from '@angular/router';
import { Project } from 'src/app/models/project';
import { Task } from 'src/app/models/task';
import { AccountService } from 'src/app/services/account.service';
import { CurrentUserInitializerService } from 'src/app/services/current-user-initializer.service';
import { UserWithRole } from 'src/app/models/user-with-role';
import { TokenService } from 'src/app/services/token.service';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.css']
})
export class ProjectDetailsComponent implements OnInit {

  selectedProject : Project;
  currentTask: Task;
  isAvailable : boolean = false;

  constructor(private service : ProjectsService, private route: ActivatedRoute, private userInitializeService: CurrentUserInitializerService, private tokenService: TokenService) {
    
   }

  ngOnInit() {
    this.selectedProject = this.route.snapshot.data['project'];
    this.userInitializeService.currentProjectId = this.selectedProject.Id;
    
    this.userInitializeService.loadCurrentUser().then(
      () => {
        this.isAvailable = true
      }
    );
  }

  public onChooseTaskEvent(task: Task){
    this.currentTask = task;
  }

}
