import { Component } from '@angular/core';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  projectLayout = true;
  taskLayout = false;
  accountLayout = false;
  title = 'Angular';

  openTaskLayout(){
    this.taskLayout = true;
    this.projectLayout = false;
    this.accountLayout = false;
  }

  openProjectLayout(){
    this.projectLayout = true;
    this.taskLayout = false;
    this.accountLayout = false;
  }
  openAccountLayout(){
    this.accountLayout = true;
    this.projectLayout = false;
    this.taskLayout = false;    
  }
}
