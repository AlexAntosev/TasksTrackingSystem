import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/core/services/account.service';
import { Account } from 'src/app/core/models/account.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  account : Account

  constructor(private service : AccountService) {
    this.account = new Account();
   }

  ngOnInit() {
  }

  onRegister(){
    this.service.Register(this.account);
  }

}
