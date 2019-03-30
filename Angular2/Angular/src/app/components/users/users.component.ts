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
  public newUserId: number;
  @Input()
  public projectId: number;

  constructor(private service: UsersService) { }

  ngOnInit() {
    this.service.getUsersByProjectId(this.projectId).subscribe(
      users => this.userList = users
    )
  }

  public addUserToProject(){
    return this.service.addUserToProject(this.newUserId, this.projectId).subscribe();
  }

  public removeUser(userId: number){
    return this.service.removeUserFromProject(userId, this.projectId).subscribe();
  }

}
