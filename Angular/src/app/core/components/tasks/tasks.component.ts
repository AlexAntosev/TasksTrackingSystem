import { Component, OnInit, Input, ViewChild, TemplateRef } from '@angular/core';
import { TaskService } from 'src/app/core/services/task.service';
import { Task } from 'src/app/core/models/task.model';
import { Project } from 'src/app/core/models/project.model';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {

  @Input() project: Project;
  selectedTask : Task;
  isNewTask :boolean;
  currentTemplate: string;

  @ViewChild("editTemplate") editTemplate: TemplateRef<any>;
  @ViewChild("listTemplate") listTemplate: TemplateRef<any>;

  constructor(private service: TaskService) { }

  ngOnInit() {
    this.getByProject(this.project);
  }

  getByProject(project: Project) {
    this.currentTemplate = "list";
    this.service.getByProject(project);
    
  }

  onCreate() {
    this.selectedTask = new Task(0, "", "", 1);
    //this.service.createTask(this.selectedTask);
    this.isNewTask = true;
    this.currentTemplate = "create";
  }

  onDelete(taskId : number){
      this.service.deleteTask(taskId).subscribe(res => this.service.getByProject(this.project));
  }

  saveTask() {
    if (this.isNewTask) {
      this.service.createTask(this.selectedTask, this.project.Id).subscribe(res => {
          this.getByProject(this.project);
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
    this.getByProject(this.project);
  }

  loadTemplate() {
    if (this.currentTemplate === "update" || this.currentTemplate === "create") {
      return this.editTemplate;
    } else if(this.currentTemplate === "list")
    return this.listTemplate;
  }

}
