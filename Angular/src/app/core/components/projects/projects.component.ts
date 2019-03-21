import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { ProjectService } from 'src/app/core/services/project.service';
import { Project } from 'src/app/core/models/project.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  constructor(private router: Router) {  }

  ngOnInit() {
  }

  onList(){
    this.router.navigate(['/projects']);
  }

  onCreate(){
    this.router.navigate(['/projects/create', {isNewProject: "true"}]);
  }
}
