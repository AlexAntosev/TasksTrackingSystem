import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Input } from '@angular/core';
import { Project } from 'src/app/models/project';
import { Output } from '@angular/core';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.css']
})
export class ProjectEditComponent implements OnInit {

  @Input()
  public project: Project;

  @Output() 
  saveEntry: EventEmitter<any> = new EventEmitter();

  constructor(private modalService: NgbModal) { }

  ngOnInit() {
    console.log(this.project);
  }

  saveProject() {
    this.saveEntry.emit(this.project);
  }

}
