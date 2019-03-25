import { Component, OnInit } from '@angular/core';
import { TaskService } from 'src/app/core/services/task.service';
import { Task } from 'src/app/core/models/task.model';

@Component({
  selector: 'app-task-create',
  templateUrl: './task-create.component.html',
  styleUrls: ['./task-create.component.css']
})
export class TaskCreateComponent implements OnInit {

  selectedTask : Task;

  constructor(private service : TaskService) { }

  ngOnInit() {
  }

  saveTask(projectId: number) {
    this.selectedTask = new Task(0, "", "", 1);
      this.service.createTask(this.selectedTask, projectId).subscribe(res => {
          this.getByProject(projectId);
      });
  }

  cancel(projectId: number) {
    this.selectedTask = null;
    this.getByProject(projectId);
  }

  getByProject(projectId: number) {
    this.service.getByProject(projectId);    
  }

}
