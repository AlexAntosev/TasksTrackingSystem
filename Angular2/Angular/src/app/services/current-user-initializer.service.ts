import { Injectable } from '@angular/core';
import { User } from 'src/app/models/user';
import { HttpClient } from '@angular/common/http';
import { TokenService } from 'src/app/services/token.service';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserInitializerService {

  public currentUser: User;
  public readonly rootURL = "http://localhost:60542";

  constructor(private http: HttpClient, private tokenService: TokenService) { 

  }

  public loadCurrentUser(): Promise<boolean> {
    return new Promise((resolve, reject) => {
      return this.http
      .get<User>(this.rootURL + '/api/Account/Current')
      .subscribe(currentUser => {
        this.currentUser = currentUser;
        console.log(currentUser);
        this.currentUser.Role = this.tokenService.fetchToken().payload.role;
        resolve(true);
      }, error => {
        if(error.status !== 401){
          throw new Error(error);
        }
        resolve(true);
      })
    })
  }
}
