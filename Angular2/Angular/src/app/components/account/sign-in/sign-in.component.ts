import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { User } from 'src/app/models/user';

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
    .subscribe(({userDTO, token}) => {
      localStorage.setItem(this.service.tokenKey, token);
      this.service.isSignIn = true;
      this.service.signInUser = userDTO as User;
      console.log(userDTO);
      console.log(token);
    });
  }
}
