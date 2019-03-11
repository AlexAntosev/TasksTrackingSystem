import { Component, OnInit } from '@angular/core';
import { ProjectService } from 'src/app/core/services/project.service';
import { NgForm } from '@angular/forms/src/directives/ng_form';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {

  constructor(private service : ProjectService) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form? : NgForm){
    if(form != null)
      form.resetForm();
    this.service.formData = {
      Id : null,
      Name : '',
      Tag : ''
    }
  }

  onSubmit(form : NgForm){
    if(form.value.Id == null)
      this.insertRecord(form);
    else
      this.updateRecord(form);
    
  }

  insertRecord(form : NgForm){
    this.service.createProject(form.value).subscribe(res => {
      this.resetForm(form);
      this.service.refreshProjectList();
    })
  }

  updateRecord(form : NgForm){
    this.service.updateProject(form.value).subscribe(res => {
      this.resetForm(form);
      this.service.refreshProjectList();
    })
  }

}
