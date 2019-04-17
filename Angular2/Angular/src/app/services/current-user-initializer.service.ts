import { Injectable } from '@angular/core';
import { User } from 'src/app/models/user';
import { HttpClient } from '@angular/common/http';
import { TokenService } from 'src/app/services/token.service';
import { Role } from 'src/app/models/role.enum';
import { UserWithRole } from 'src/app/models/user-with-role';
import { UserRole } from 'src/app/models/user-role';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserInitializerService {

  public currentUserWithRole: UserWithRole;
  public currentProjectId: number;
  public readonly rootURL = "http://localhost:60542";

  constructor(private http: HttpClient, private tokenService: TokenService) {

  }

  public loadCurrentUser(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      return this.http
        .get<UserWithRole>(this.rootURL + '/api/Account/Current/' + this.currentProjectId)
        .subscribe(currentUserwithRole => {
          this.currentUserWithRole = currentUserwithRole as UserWithRole;
          this.currentUserWithRole.User.Role = this.tokenService.fetchToken().payload.role;
          resolve(true);
        }, error => {
          if (error.status !== 401) {
            throw new Error(error);
          }
          resolve(true);
        })
    })
  }
}

