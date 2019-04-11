import { Component, OnInit } from '@angular/core';
import { Task } from 'src/app/models/task';
import { TasksService } from 'src/app/services/tasks.service';
import { Input } from '@angular/core';
import { Priority } from 'src/app/models/priority.enum';
import { UsersService } from 'src/app/services/users.service';
import { User } from 'src/app/models/user';
import { Output } from '@angular/core';
import { EventEmitter } from '@angular/core';

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

  public orderByField = 'Name';
  public reverseSort = false;

  @Input()
  public projectId: number;

  @Output()
  public chooseTaskEventEmitter = new EventEmitter<Task>();

  constructor(private service: TasksService, private userService: UsersService) { }

  ngOnInit() {
    this.RefreshTasks();
    this.RefreshUsers();
  }

  ngOnChanges() {
    this.RefreshUsers();
  }

  public createTask(): void {    
    this.service.createTask(this.newTaskName, this.newTaskDescription, this.newTaskPriority,this.projectId, this.newUserExecutorId)
      .subscribe(
      createdTask => {
        this.RefreshTasks();
        this.RefreshCreatingModel();
      })
  }

  public deleteTask(taskId: number): void {
    this.service.deleteTask(taskId)
      .subscribe(() => {        
        this.taskList = this.taskList.filter(task => task.Id !== taskId)
      })
  } 

  public chooseTask(task: Task): void {
    this.chooseTaskEventEmitter.emit(task);
  }

  public orderBy(field: string): void {
    if(field === 'Name')
      this.taskList.sort((a,b)=> {return a.Name > b.Name ? 1 : -1});
    else if(field === 'Priority')
      this.taskList.sort((a,b)=> {return a.Priority < b.Priority ? 1 : -1});
  }

  private RefreshTasks(): void{
    this.service.getTasksByProjectId(this.projectId).subscribe(
      tasks => this.taskList = tasks
    )
  }

  private RefreshUsers(): void{
    this.userService.getUsersByProjectId(this.projectId).subscribe(
      (users) => {
        this.usersInProject = users;
      }
    )
  }

  private RefreshCreatingModel(): void{
    this.newTaskName = null;
    this.newTaskDescription = null;
    this.newTaskPriority = null;
    this.newUserExecutorId = null;
  }
}
