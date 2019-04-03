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
  

  constructor(private service: AccountService) { }

  ngOnInit() {
  }

  public signIn() {
    this.service.signIn(this.newEmail, this.newPassword)
    .subscribe(data => {
      sessionStorage.setItem(this.service.tokenKey, data.access_token);
      this.service.isSignIn = true;
      console.log(data.access_token);
    });
  }
}
