import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/core/services/account.service';
import { Account } from 'src/app/core/models/account.model';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  

  constructor(private service : AccountService) { }

  ngOnInit() {
  }

  onLogout(){
    this.service.Logout();
  }
}
