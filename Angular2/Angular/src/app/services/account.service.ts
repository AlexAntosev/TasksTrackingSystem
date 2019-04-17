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
import { ErrorService } from 'src/app/services/error.service';
import { UserWithRole } from 'src/app/models/user-with-role';
import { Role } from 'src/app/models/role.enum';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  public readonly rootURL = "http://localhost:60542";
  public tokenSignature: string = "tokenInfo";
  public isSignIn: boolean;
  public signInUser: User;

  private currentUserWithRole$: BehaviorSubject<UserWithRole>;

  constructor(private http: HttpClient,
    private tokenService: TokenService,
    private currentUserInitializerService: CurrentUserInitializerService,
    private router: Router,
    private errorService: ErrorService) {
    this.currentUserWithRole$ = new BehaviorSubject(currentUserInitializerService.currentUserWithRole)
    console.log(currentUserInitializerService.currentUserWithRole);
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
      }),
      catchError(this.errorService.handleError)

      );
  }

  public signOut() {
    return this.http.post(this.rootURL + '/api/Account/SignOut', {})
      .pipe(
      catchError(this.errorService.handleError))
      .subscribe(() => {
        this.tokenService.clearToken();
        this.currentUserWithRole$.next(null);
        this.router.navigate(['/sign-in']);
      }
      );
  }

  public getCurrentUserWithRole(): UserWithRole {
    return this.currentUserInitializerService.currentUserWithRole as UserWithRole;
  }

  public profile(userName: string): Observable<User> {
    return this.http.get<User>(this.rootURL + '/api/Users/UserName/' + userName, { params: { userName: userName } })
      .pipe(
      catchError(this.errorService.handleError)
      );
  }

  private handleUserAndToken(user: User, token: string): void {
    this.tokenService.KeepToken(token);
    user.Role = this.tokenService.fetchToken().payload.role;
    let userWithRole = {
      User: user,
      Role: Role.Watcher
    }
    this.currentUserWithRole$.next(userWithRole as UserWithRole);
  }

  public isSignedIn(): Observable<boolean> {
    return this.currentUserWithRole$.pipe(
      map(currentUserWithRole => !!currentUserWithRole)
    )
  }

  public getGlobalRole(): Observable<UserRole> {
    return this.currentUserWithRole$.pipe(
      map(currentUserWithRole => currentUserWithRole ? currentUserWithRole.User.Role : null)
    )
  }
}
