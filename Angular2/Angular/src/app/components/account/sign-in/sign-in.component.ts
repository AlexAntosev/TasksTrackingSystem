import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { User } from 'src/app/models/user';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { error } from 'selenium-webdriver';
import { AlertService } from 'src/app/services/alert.service';
import { FormGroup } from '@angular/forms/src/model';
import { Validators } from '@angular/forms';
import { FormBuilder } from '@angular/forms';


@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  public newEmail: string;
  public newPassword: string;
  public loginForm: FormGroup;

  constructor(private formBuilder: FormBuilder , private service: AccountService, private router: Router, private alertService: AlertService) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      Email: ['', Validators.required],
      Password: ['', Validators.required]
    })
  }

  public signIn(): void {
    this.service.signIn(this.newEmail, this.newPassword)
      .subscribe(data => {
        this.router.navigate(['/home']);
      },
      error => {
        this.alertService.error(error);
      });
  }
}
