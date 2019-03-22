import { Component, OnInit } from '@angular/core';
import { Project } from 'src/app/core/models/project.model';
import { ProjectService } from 'src/app/core/services/project.service';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.css']
})
export class ProjectDetailsComponent implements OnInit {

  selectedProject : Project;
  selectedId : number;

  constructor(private service : ProjectService, private route: ActivatedRoute) {
    
   }

  ngOnInit() {
    this.selectedId = +this.route.snapshot.paramMap.get("id");
    this.service.getProject(this.selectedId);
  }

}
