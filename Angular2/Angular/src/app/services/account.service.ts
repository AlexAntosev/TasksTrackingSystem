import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SignUp } from 'src/app/models/sign-up';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { tap } from 'rxjs/operators';
import { User } from 'src/app/models/user';
import { TokenService } from 'src/app/services/token.service';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { CurrentUserInitializerService } from 'src/app/services/current-user-initializer.service';
import { map } from 'rxjs/operators';
import { UserRole } from 'src/app/models/user-role';
import { catchError } from 'rxjs/internal/operators/catchError';
import { of } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  public readonly rootURL = "http://localhost:60542";
  public tokenSignature: string = "tokenInfo";
  public isSignIn: boolean;
  public signInUser: User;

  private currentUser$: BehaviorSubject<User>;

  constructor(private http: HttpClient, private tokenService: TokenService, private currentUserInitializerService: CurrentUserInitializerService, private router: Router) {
    this.currentUser$ = new BehaviorSubject(currentUserInitializerService.currentUser)
    console.log(currentUserInitializerService.currentUser);
  }

  public signUp(user: SignUp): Observable<SignUp> {

    return this.http.post<any>(this.rootURL + '/api/Account/SignUp', user)
      .pipe(
      tap(({ user, token }) => {
        this.handleUserAndToken(user, token);
      })
      );
  }

  public signIn(userName: string, password: string): any {
    return this.http.post<any>(this.rootURL + '/api/Account/SignIn', { userName, password })
      .pipe(
      tap(({ user, token }) => {
        this.handleUserAndToken(user, token);
      })
    );
  }

  public signOut() {
    return this.http.post(this.rootURL + '/api/Account/SignOut', {})
      .subscribe(() => {
        this.tokenService.clearToken();
        this.currentUser$.next(null);
      }
      );
  }

  public getCurrentUser(): Observable<User> {
    return this.currentUser$.asObservable();
  }

  public profile(userName: string): Observable<User> {
    return this.http.get<User>(this.rootURL + '/api/Users/' + userName, { params: { userName: userName } });
  }

  private handleUserAndToken(user: User, token: string): void {
    this.tokenService.KeepToken(token);
    user.Role = this.tokenService.fetchToken().payload.role;
    console.log(user);
    this.currentUser$.next(user);
  }

  public isSignedIn(): Observable<boolean> {
    return this.currentUser$.pipe(
      map(currentUser => !!currentUser)
    )
  }

  public getRole(): Observable<UserRole> {
    return this.currentUser$.pipe(
      map(currentUser => currentUser ? currentUser.Role : null)
    )
  }
}
