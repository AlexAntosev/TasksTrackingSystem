import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Account } from 'src/app/core/models/account.model';
import { HttpHeaders } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  readonly rootURL = "http://localhost:60542/api/Account";
  headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
  loginHeaders = new HttpHeaders({ 'Content-Type': 'x-www-form-urlencoded' });
  
  tokenKey: string = "tokenInfo";

  constructor(private http: HttpClient) { }

  Register(formData: Account) {
    return this.http.post(this.rootURL + '/Register', JSON.stringify({ Email: formData.Email, Password: formData.Password, ConfirmPassword: formData.ConfirmPassword }), { headers: this.headers })
      .subscribe();
  }

  Login(formData: Account) {
    console.log(formData);
    //return this.http.post('http://localhost:60542/Token', { grant_type: 'authorization_code', username: formData.Email, password: formData.Password },{ headers: this.loginHeaders })
    return this.http.post<any>('http://localhost:60542/Token', "userName=" + formData.Email +
    "&password=" + formData.Password +
    "&grant_type=password",{ headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
      .subscribe(data => {
        debugger;
        sessionStorage.setItem(this.tokenKey, data.access_token);
        console.log(data.access_token);
      }
    );
  }
}

