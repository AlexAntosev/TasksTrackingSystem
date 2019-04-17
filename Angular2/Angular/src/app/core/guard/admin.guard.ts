import { Injectable } from "@angular/core";
import { CanActivate, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { map, tap } from "rxjs/operators";
import { Observable } from "rxjs";
import { AccountService } from 'src/app/services/account.service';

@Injectable()
export class AdminGuard implements CanActivate, CanActivateChild {
    
    constructor(private accountService: AccountService) {
    }

    public canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        return this.isAdmin();
    }

    public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        return this.isAdmin();
    }

    private isAdmin(): Observable<boolean> {
        return this.accountService.getGlobalRole().pipe(
            map(role => role === 'Admin'),
            tap(isAdmin => console.log(isAdmin))
        );
    }
}
