import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/core/services/account.service';
import { Account } from 'src/app/core/models/account.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  account: Account;

  constructor(private service: AccountService) { 
    this.account = new Account();
  }

  ngOnInit() {
  }

  onLogin() {
    this.service.Login(this.account);
  }

  

}
