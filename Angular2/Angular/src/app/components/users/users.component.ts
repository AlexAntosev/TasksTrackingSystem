import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { UsersService } from 'src/app/services/users.service';
import { Input } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { SearchingFilterPipe } from 'src/app/core/pipes/searching-filter.pipe';
import { InvitesService } from 'src/app/services/invites.service';
import { Invite } from 'src/app/models/invite';
import { AccountService } from 'src/app/services/account.service';
import { formatDate } from '@angular/common';

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
  public inviteList: Invite[];

  constructor(private service: UsersService, private inviteService: InvitesService, private accountService: AccountService) { }

  ngOnInit() {
    this.service.getUsersByProjectId(this.projectId).subscribe(
      users => this.userList = users
    );
    
    //all users
    this.service.getAllUsers().subscribe(
      users => this.allUsers = users
    );
  }

  public sendInviteToUser() {
    let invite: any = {
      AuthorId: this.accountService.getCurrentUser().Id,  
      ProjectId: this.projectId,  
      ReceiverId: this.newUserId,  
      Time: formatDate(Date.now(), 'yyyy-MM-dd', 'en')
    }
    debugger;
    this.inviteService.sendInvite(invite as Invite).subscribe(
      invite => {
        this.inviteService.getAllInvitesByProjectId(this.projectId).subscribe(
          invites => {
            this.inviteList = invites;
          }
        )
      }
    );
  }

  public deleteInvite(inviteId: number) {
    this.inviteService.deleteInvite(inviteId).subscribe(
      invite => {
        this.inviteList = this.inviteList.filter(i => i.Id !== inviteId)
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
