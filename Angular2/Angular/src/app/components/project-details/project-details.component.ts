import { Component, OnInit } from '@angular/core';
import { ProjectsService } from 'src/app/services/projects.service';
import { ActivatedRoute } from '@angular/router';
import { Project } from 'src/app/models/project';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.css']
})
export class ProjectDetailsComponent implements OnInit {

  selectedProject : Project;

  constructor(private service : ProjectsService, private route: ActivatedRoute) {
    
   }

  ngOnInit() {
    this.selectedProject = this.route.snapshot.data['project'];
    console.log(this.selectedProject);
  }

}
