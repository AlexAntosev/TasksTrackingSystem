import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  public newEmail: string;
  public newPassword: string;
  public tokenKey: string = "tokenInfo";

  constructor(private service: AccountService) { }

  ngOnInit() {
  }

  public signIn() {
    this.service.signIn(this.newEmail, this.newPassword)
    .subscribe(data => {
      sessionStorage.setItem(this.tokenKey, data.access_token);
      console.log(data.access_token);
    });
  }
}
