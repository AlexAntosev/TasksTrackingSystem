import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { SignUp } from 'src/app/models/sign-up';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  public newUserName: string;
  public newFirstName: string;
  public newLastName: string;
  public newPosition: string;
  public newEmail: string;
  public newPassword: string;
  public newConfirmPassword: string;

  constructor(private service: AccountService, private router: Router) { }

  ngOnInit() {
  }

  singUp() {
    let newUser: SignUp = {
      UserName: this.newUserName,
      FirstName: this.newFirstName,
      LastName: this.newLastName,
      Position: this.newPosition,
      Email: this.newEmail,
      Password: this.newPassword,
      ConfirmPassword: this.newConfirmPassword
    }
    this.service.signUp(newUser).subscribe(() => {
      this.router.navigate(['/home']);
    });
  }
}
