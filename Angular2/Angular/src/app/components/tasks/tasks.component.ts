import { Component, OnInit } from '@angular/core';
import { Task } from 'src/app/models/task';
import { TasksService } from 'src/app/services/tasks.service';
import { Input } from '@angular/core';
import { Priority } from 'src/app/models/priority.enum';
import { UsersService } from 'src/app/services/users.service';
import { User } from 'src/app/models/user';

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
  public newUserExecutorId: number;
  public usersInProject: User[];

  @Input()
  public projectId: number;

  constructor(private service: TasksService, private userService: UsersService) { }

  ngOnInit() {
    this.service.getTasksByProjectId(this.projectId).subscribe(
      tasks => this.taskList = tasks
    )
    this.userService.getUsersByProjectId(this.projectId).subscribe(
      (users) => {
        debugger;
        this.usersInProject = users;
      }
    )
  }

  public createTask() {
    this.service.createTask(this.newTaskName, this.newTaskDescription, this.newTaskPriority,this.projectId, this.newUserExecutorId)
      .subscribe(
      createdTask => {
        this.service.getTasksByProjectId(this.projectId).subscribe(
          tasks => this.taskList = tasks
        )
      })
  }

  public deleteTask(taskId: number) {
    this.service.deleteTask(taskId)
      .subscribe(() => {        
        this.taskList = this.taskList.filter(task => task.Id !== taskId)
      })
  } 

}
