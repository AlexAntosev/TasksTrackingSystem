import { Component, OnInit } from '@angular/core';
import { ProjectsService } from 'src/app/services/projects.service';
import { ActivatedRoute } from '@angular/router';
import { Project } from 'src/app/models/project';
import { Task } from 'src/app/models/task';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.css']
})
export class ProjectDetailsComponent implements OnInit {

  selectedProject : Project;
  currentTask: Task;

  constructor(private service : ProjectsService, private route: ActivatedRoute) {
    
   }

  ngOnInit() {
    this.selectedProject = this.route.snapshot.data['project'];
    console.log(this.selectedProject);
  }

  public onChooseTaskEvent(task: Task){
    this.currentTask = task;
  }

}
