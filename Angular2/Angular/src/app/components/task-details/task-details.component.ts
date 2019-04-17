import { Component, OnInit } from '@angular/core';
import { Task } from 'src/app/models/task';
import { ActivatedRoute } from '@angular/router';
import { EventEmitter } from '@angular/core/src/event_emitter';
import { Output } from '@angular/core';
import { TasksService } from 'src/app/services/tasks.service';
import { Input } from '@angular/core';
import { UsersService } from 'src/app/services/users.service';
import { User } from 'src/app/models/user';
import { Type } from 'src/app/models/type.enum';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TaskEditComponent } from 'src/app/components/tasks/task-edit/task-edit.component';

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
    private userService: UsersService,
    private modalService: NgbModal) {

  }

  ngOnInit() {
  }

  public openEditModal(task: Task) {

    const newTask: any = {
      Name: task.Name,
      Description: task.Description,
      Priority: task.Priority,
      Type: task.Type,
      Status: task.Status,
      Deadline: task.Deadline,
      Created: task.Created,
      Updated: task.Updated,
      ProjectId: task.ProjectId,
      CreatorId: task.CreatorId,
      ExecutorId: task.ExecutorId,
    }
    const modalRef = this.modalService.open(TaskEditComponent);
    modalRef.componentInstance.task = newTask;
    modalRef.componentInstance.editing = true;
    modalRef.componentInstance.saveEntry
      .subscribe(
      (t) => {
        this.taskService.editTask(task.Id, t).subscribe();
      });
  }  
}
