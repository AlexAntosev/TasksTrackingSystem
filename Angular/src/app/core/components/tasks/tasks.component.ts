import { Component, OnInit, Input, ViewChild, TemplateRef } from '@angular/core';
import { TaskService } from 'src/app/core/services/task.service';
import { Task } from 'src/app/core/models/task.model';
import { Project } from 'src/app/core/models/project.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {

  @Input() projectId: number;

  constructor(private service: TaskService, private router : Router) { }

  ngOnInit() {
    this.service.getByProject(this.projectId);
    this.service.projectId = this.projectId;
  } 

  onCreate() {
    this.service.createTask(new Task(0,"","",0), this.projectId);
  }
}
