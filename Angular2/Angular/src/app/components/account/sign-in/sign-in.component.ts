import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { User } from 'src/app/models/user';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  public newEmail: string;
  public newPassword: string;
  

  constructor(private service: AccountService, private router: Router) { }

  ngOnInit() {
  }

  public signIn(): void {
    this.service.signIn(this.newEmail, this.newPassword)
    .subscribe(() => {  
      this.router.navigate(['/projects']);
    });
  }
}
