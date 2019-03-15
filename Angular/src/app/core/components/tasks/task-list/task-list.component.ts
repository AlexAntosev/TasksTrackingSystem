import { Component, OnInit } from '@angular/core';
import { TaskService } from 'src/app/core/services/task.service';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {

  projectId : number;

  constructor(private service : TaskService) { }

  ngOnInit() {
  }

  getByProjectId(projectId :number){
    this.service.getByProjectId(this.projectId);
  }
}
