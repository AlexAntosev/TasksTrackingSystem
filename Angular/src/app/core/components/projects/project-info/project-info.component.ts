import { Component, OnInit, Input } from '@angular/core';
import { Project } from 'src/app/core/models/project.model';

@Component({
  selector: 'app-project-info',
  templateUrl: './project-info.component.html',
  styleUrls: ['./project-info.component.css']
})
export class ProjectInfoComponent implements OnInit {

  @Input() project : Project;

  constructor() { }

  ngOnInit() {
  }

  resetProject(){
    this.project = null;
  }

}
