import { Component, OnInit, Input, ViewChild, TemplateRef } from '@angular/core';
import { TaskService } from 'src/app/core/services/task.service';
import { Task } from 'src/app/core/models/task.model';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {

  @Input() projectId: number;
  selectedTask : Task;
  isNewTask :boolean;
  currentTemplate: string;

  @ViewChild("editTemplate") editTemplate: TemplateRef<any>;

  constructor(private service: TaskService) { }

  ngOnInit() {
    this.getByProjectId(this.projectId);
    
  }

  getByProjectId(projectId: number) {
    this.service.getByProjectId(projectId);
  }

  onCreate() {
    this.selectedTask = new Task(0, "", "");
    //this.service.createTask(this.selectedTask);
    this.isNewTask = true;
    this.currentTemplate = "create";
    console.log(this.selectedTask);
  }

  onDelete(taskId : number){
      this.service.deleteTask(taskId).subscribe(res => this.service.getByProjectId(this.projectId));
  }

  saveTask() {
    if (this.isNewTask) {
      this.service.createTask(this.selectedTask, this.projectId).subscribe(data => {
          this.getByProjectId(this.projectId);
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
    this.getByProjectId(this.projectId);
  }

  loadTemplate() {
    if (this.currentTemplate === "update" || this.currentTemplate === "create") {
      return this.editTemplate;
    } 
  }

}
