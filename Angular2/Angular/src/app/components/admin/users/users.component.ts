import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersForAdminComponent implements OnInit {

  public users: User[];
  
    constructor(private userService: UsersService) { }
  
    ngOnInit() {
      this.userService.getAllUsers().subscribe(users => {
        this.users = users;
      });
    }
  
    public deleteUser(userId: number): void {
      this.userService.deleteUser(userId)
        .subscribe(() => {
          this.users = this.users.filter(user => user.Id !== userId);
        });
    }
}
