import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { ProjectService } from 'src/app/core/services/project.service';
import { Project } from 'src/app/core/models/project.model';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  selectedProject: Project;
  projects: Project[];
  isNewProject : boolean;
  currentTemplate : string;

  //типы шаблонов
  @ViewChild('listTemplate') listTemplate: TemplateRef<any>;
  @ViewChild('editTemplate') editTemplate: TemplateRef<any>;
  @ViewChild('selectedTemplate') selectedTemplate: TemplateRef<any>;

  constructor(private service: ProjectService) {
  }

  ngOnInit() {
    this.service.getProjects();
  }

  getProjects() {
    this.service.getProjects();
    this.currentTemplate = "list";
  }

  onSelect(project: Project){
    console.log(project);
    this.service.getProject(project.Id);
    this.selectedProject = project;
    this.currentTemplate = "selected";
  }

  onCreate() {
    this.selectedProject = new Project(0, "", "");
    //this.service.createProject(this.selectedProject);
    this.isNewProject = true;
    this.currentTemplate = "create";
  }

  onUpdate(project: Project) {
    this.isNewProject = false;
    this.selectedProject = project;
    this.currentTemplate = "update";
  }

  onDelete(projectId: number) {
    //if(this.service.formData.Tasks.length = 0)
    
    this.service.deleteProject(projectId).subscribe(res => {
      this.service.getProjects();
    })
  }

  saveProject() {
    if (this.isNewProject) {
      this.service.createProject(this.selectedProject).subscribe(data => {
          this.getProjects();
      });
      this.isNewProject = false;
      this.selectedProject = null;
    } else {
      this.service.updateProject(this.selectedProject).subscribe(data => {
          this.getProjects();
      });
      this.selectedProject = null;
    }
  }

  cancel() {
    this.selectedProject = null;
    this.getProjects();
  }

  loadTemplate() {
    if (this.currentTemplate === "update" || this.currentTemplate === "create") {
      return this.editTemplate;
    } else if(this.currentTemplate === "selected"){
      return this.selectedTemplate;
    }
    else{
      return this.listTemplate;
    }
  }
}
