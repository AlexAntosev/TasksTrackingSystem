import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Account } from 'src/app/core/models/account.model';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  readonly rootURL = "http://localhost:60542/api/Account";
  headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'})

  constructor(private http: HttpClient) { }

  Register(formData: Account) {
    return this.http.post(this.rootURL + '/Register', JSON.stringify({ Email: formData.Email, Password: formData.Password, ConfirmPassword: formData.ConfirmPassword }) , { headers: this.headers})
    .subscribe();
  }
}
