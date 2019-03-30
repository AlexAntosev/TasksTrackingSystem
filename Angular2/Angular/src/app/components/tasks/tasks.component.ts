import { Component, OnInit } from '@angular/core';
import { Task } from 'src/app/models/task';
import { TasksService } from 'src/app/services/tasks.service';
import { Input } from '@angular/core';
import { Priority } from 'src/app/models/priority.enum';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {

  public taskList: Task[];
  public newTaskName: string;
  public newTaskDescription: string;
  public newTaskPriority: Priority;

  @Input()
  public projectId: number;

  constructor(private service: TasksService) { }

  ngOnInit() {
    this.service.getTasksByProjectId(this.projectId).subscribe(
      tasks => this.taskList = tasks
    )
  }

  public createTask() {
    this.service.createTask(this.newTaskName, this.newTaskDescription, this.newTaskPriority,this.projectId)
      .subscribe(
      createdTask => {
        const task = <Task>{
          Id: createdTask.Id,
          Name: createdTask.Name,
          Description: createdTask.Description,
          Priority: createdTask.Priority
        };
        this.taskList.push(task);
      })
  }

  public deleteTask(taskId: number) {
    this.service.deleteTask(taskId)
      .subscribe(() => {
        this.taskList = this.taskList.filter(task => task.Id !== taskId)
      })
  } 

}
