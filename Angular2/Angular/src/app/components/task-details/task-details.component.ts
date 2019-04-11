import { Component, OnInit } from '@angular/core';
import { Task } from 'src/app/models/task';
import { ActivatedRoute } from '@angular/router';
import { EventEmitter } from '@angular/core/src/event_emitter';
import { Output } from '@angular/core';
import { TasksService } from 'src/app/services/tasks.service';
import { Input } from '@angular/core';
import { UsersService } from 'src/app/services/users.service';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrls: ['./task-details.component.css']
})
export class TaskDetailsComponent implements OnInit {

  @Input()
  selectedTask: Task;

  constructor(private route: ActivatedRoute,
     private taskService: TasksService,
     private userService: UsersService) {
    
   }

  ngOnInit() {
  }
}
