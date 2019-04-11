import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { UsersService } from 'src/app/services/users.service';
import { Input } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  public userList: User[];
  public allUsers: User[];
  public newUserId: number;
  @Input()
  public projectId: number;

  constructor(private service: UsersService) { }

  ngOnInit() {
    this.service.getUsersByProjectId(this.projectId).subscribe(
      users => this.userList = users
    );
    
    //all users
    this.service.getAllUsers().subscribe(
      users => this.allUsers = users
    );
  }

  public addUserToProject(){
    debugger;
    this.service.addUserToProject(this.newUserId, this.projectId)
    .subscribe(
      user => {
        this.service.getUsersByProjectId(this.projectId)
        .subscribe((users) => {
            this.userList = users
          }
        );
      }
    );
  }

  public removeUser(userId: number) {
    this.service.removeUserFromProject(userId, this.projectId)
    .subscribe(
      () => {
        this.userList = this.userList.filter(u => u.Id !== userId)
      }
    );
  }

}
