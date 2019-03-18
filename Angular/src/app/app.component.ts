import { Component } from '@angular/core';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  projectLayout = true;
  taskLayout = false;
  title = 'Angular';

  openTaskLayout(){
    this.taskLayout = true;
    this.projectLayout = false;
  }

  openProjectLayout(){
    this.projectLayout = true;
    this.taskLayout = false;
  }
}
