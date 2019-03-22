import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/core/services/project.service';
import { Project } from 'src/app/core/models/project.model';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.css']
})
export class ProjectEditComponent implements OnInit {

  isNewProject: boolean;
  selectedProject: Project;
  selectedId: number;


  constructor(private service: ProjectService, private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.route.data
      .subscribe((data => {
        this.isNewProject = data.isNewProject;
      }));
    if (!this.isNewProject) {
      this.selectedId = +this.route.snapshot.paramMap.get("id");
      this.service.getProject(this.selectedId).subscribe(data => this.selectedProject = data);
    }
    else
      this.selectedProject = new Project(0, "", "");
  }

  saveProject() {
    if (this.isNewProject) {

      this.service.createProject(this.selectedProject).subscribe(res => {
        //this.service.getProjects();

      });
      this.isNewProject = false;
      this.selectedProject = null;
    } else {
      this.service.updateProject(this.selectedProject).subscribe(data => {
        this.router.navigate(['/projects']);
      });
      this.selectedProject = null;
    }

  }

  RedirectToProjectList() {
    this.service.getProjects();
    this.router.navigate(['/projects']);
  }

  cancel() {
    this.selectedProject = null;
    this.router.navigate(['/projects']);
  }

}
