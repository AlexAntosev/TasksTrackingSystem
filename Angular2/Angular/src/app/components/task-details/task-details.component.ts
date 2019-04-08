import { Component, OnInit } from '@angular/core';
import { Task } from 'src/app/models/task';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrls: ['./task-details.component.css']
})
export class TaskDetailsComponent implements OnInit {

  selectedTask: Task;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.selectedTask = this.route.snapshot.data['task'];
  }

}
