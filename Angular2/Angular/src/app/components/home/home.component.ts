import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UsersService } from 'src/app/services/users.service';
import { CurrentUserInitializerService } from 'src/app/services/current-user-initializer.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public isAvailable: boolean = false;

  constructor(private router: Router,
    private accountService: AccountService,
    private modalServie: NgbModal,
    private userService: UsersService,
    private userInitializeService: CurrentUserInitializerService) { }

  ngOnInit() {
    this.userInitializeService.currentProjectId = 0;
    this.userInitializeService.loadCurrentUser().then(
      () => {
        this.isAvailable = true
      }
    );
  }

  public getUserId(){
    return this.accountService.getCurrentUserWithRole().User.Id;
  }

}
