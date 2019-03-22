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
  selectedTask : Task;
  isNewTask :boolean;

  constructor(private service: TaskService, private router : Router) { }

  ngOnInit() {
    this.getByProject();
    //this.router.navigate(['/projects/details/'+this.projectId, {id: this.projectId}]);
  }

  getByProject() {
    this.service.refreshTaskList(this.projectId);    
  }

  onCreate() {
    this.selectedTask = new Task(0, "", "", 1);
    //this.service.createTask(this.selectedTask);
    this.isNewTask = true;
  }

  onDelete(taskId : number){
      this.service.deleteTask(taskId).subscribe(res => this.service.refreshTaskList(this.projectId));
  }

  saveTask() {
    if (this.isNewTask) {
      this.service.createTask(this.selectedTask, this.projectId).subscribe(res => {
          this.getByProject();
      });
      this.isNewTask = false;
      this.selectedTask = null;
      
    } else {
      //this.service.updateProject(this.selectedProject).subscribe(data => {
          //this.getProjects();
      ;
      this.selectedTask = null;
    }
  }

  cancel() {
    this.selectedTask = null;
    this.getByProject();
  }

}
