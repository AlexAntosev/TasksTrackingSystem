import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SignUp } from 'src/app/models/sign-up';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { tap } from 'rxjs/operators';
import { User } from 'src/app/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  public readonly rootURL = "http://localhost:60542";
  public tokenKey: string = "tokenInfo";
  public isSignIn: boolean;
  public signInUser: User;

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
      { headers: loginHeaders })
      .pipe(
        tap((user)=> {
          debugger;
          this.signInUser = user as User;          
        })
      );
  }

  public signOut(): Observable<any>{
    sessionStorage.removeItem(this.tokenKey);    
    this.isSignIn = false;
    this.signInUser = null;
    return this.http.post(this.rootURL + '/api/Account/Logout', {});
  }

  public profile(userName: string): Observable<User>{
    debugger;
    return this.http.get<User>(this.rootURL + '/api/Users/'+ userName, {params: {userName:userName}});
  }
}
