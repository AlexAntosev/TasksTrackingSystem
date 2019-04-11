import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  public get isUserSignedIn$(): Observable<boolean> {
    return this.service.isSignedIn();
  }

  constructor(private service: AccountService, private router: Router) { }

  ngOnInit() {
  }

  public signOut(){
    return this.service.signOut();   
  }

  public Profile(){
    this.service.profile(this.service.signInUser.UserName).subscribe(
      user => {
        console.log(user);
      }
    );
  }
}