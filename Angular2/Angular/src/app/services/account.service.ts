import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SignUp } from 'src/app/models/sign-up';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  public readonly rootURL = "http://localhost:60542";

  constructor(private http: HttpClient) { }

  public signUp(user: SignUp): Observable<SignUp> {
    const registerHeaders = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    return this.http.post<SignUp>(this.rootURL + '/api/Account/Register', user, { headers: registerHeaders });
  }

  public signIn(email: string, password: string): Observable<any> {
    const loginHeaders = new HttpHeaders({ 'Content-Type': 'x-www-form-urlencoded' });
    return this.http.post<any>(this.rootURL + '/Token',
      "userName=" + email +
      "&password=" + password +
      "&grant_type=password",
      { headers: loginHeaders });
  }
}
