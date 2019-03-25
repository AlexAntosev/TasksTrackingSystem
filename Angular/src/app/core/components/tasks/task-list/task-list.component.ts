import { Component, OnInit } from '@angular/core';
import { TaskService } from 'src/app/core/services/task.service';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {

  constructor(private service : TaskService) { }

  ngOnInit() {
  }

  onDelete(taskId : number){
    this.service.deleteTask(taskId).subscribe(res => this.service.getByProject(this.service.projectId));
  }

  onSelect(taskId : number){
    
  }
}
