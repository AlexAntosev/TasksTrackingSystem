import { Component, OnInit } from '@angular/core';
import { Invite } from 'src/app/models/invite';
import { InvitesService } from 'src/app/services/invites.service';
import { Input } from '@angular/core';

@Component({
  selector: 'app-invites',
  templateUrl: './invites.component.html',
  styleUrls: ['./invites.component.css']
})
export class InvitesComponent implements OnInit {

  public inviteList: Invite[];
  @Input()
  public currentUserId: number;

  constructor(private inviteService: InvitesService) { }

  ngOnInit() {
    this.RefreshInvites();
  }

  public submitInvite(invite: Invite): void {
    this.inviteService.addUserToProject(invite).subscribe(
      (user) => {
        this.cancelInvite(invite.Id);
      }
    );
  }

  public cancelInvite(inviteId: number): void {
    this.inviteService.deleteInvite(inviteId).subscribe(
      (invite) => {
        this.inviteList = this.inviteList.filter(i => i.Id !== inviteId);
      }
    );
  }

  public RefreshInvites() {
    this.inviteService.getAllInvitesByReceiverId(this.currentUserId)
    .subscribe(
      invites => {
        this.inviteList = invites;
      }
    )
  }
}