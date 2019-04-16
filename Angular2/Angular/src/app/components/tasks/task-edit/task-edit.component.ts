import { Component, OnInit } from '@angular/core';
import { Task } from 'src/app/models/task';
import { Input } from '@angular/core';
import { Output } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { User } from 'src/app/models/user';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-task-edit',
  templateUrl: './task-edit.component.html',
  styleUrls: ['./task-edit.component.css']
})
export class TaskEditComponent implements OnInit {

  @Input()
  public task: Task;
  public usersInProject: User[];

  @Output() 
  saveEntry: EventEmitter<any> = new EventEmitter();

  constructor(private modalService: NgbModal, private userService: UsersService) { }

  ngOnInit() {
    console.log(this.task);
    this.RefreshUsers();
  }

  public saveTask() {
    this.saveEntry.emit(this.task);
    this.modalService.dismissAll();
  }

  private RefreshUsers(): void{
    this.userService.getUsersByProjectId(this.task.ProjectId).subscribe(
      (users) => {
        this.usersInProject = users;
      }
    )
  }

}
