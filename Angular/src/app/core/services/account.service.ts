import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Account } from 'src/app/core/models/account.model';
import { HttpHeaders } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  readonly rootURL = "http://localhost:60542";
  registerHeaders = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
  loginHeaders = new HttpHeaders({ 'Content-Type': 'x-www-form-urlencoded' });
  tokenKey: string = "tokenInfo";
  isLogin: boolean = false;
  userName: string;

  constructor(private http: HttpClient) { }

  Register(formData: Account) {
    return this.http.post(this.rootURL + '/api/Account/Register', formData, { headers: this.registerHeaders })
      .subscribe();
  }

  Login(formData: Account) {
    return this.http.post<any>(this.rootURL + '/Token',
      "userName=" + formData.Email +
      "&password=" + formData.Password +
      "&grant_type=password",
      { headers: this.loginHeaders })
      .subscribe(data => {
        sessionStorage.setItem(this.tokenKey, data.access_token);
        console.log(data.access_token);
        this.isLogin = true;
        this.userName = formData.Email;
      }
      );
  }

  Logout(){
    sessionStorage.removeItem(this.tokenKey);
    this.isLogin = false;
  }
}

