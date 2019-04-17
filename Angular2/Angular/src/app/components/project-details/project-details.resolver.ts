import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { Project } from 'src/app/models/project';
import { ProjectsService } from 'src/app/services/projects.service';
import { Observable } from 'rxjs';
import { ActivatedRouteSnapshot } from '@angular/router';
import { RouterStateSnapshot } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';

@Injectable()
export class ProjectDetailsResolver implements Resolve<Project> {

    constructor(private service: ProjectsService, private accountService: AccountService) {
    }

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Project> {
        return this.service.getProject(+route.params['id']);
    }

}