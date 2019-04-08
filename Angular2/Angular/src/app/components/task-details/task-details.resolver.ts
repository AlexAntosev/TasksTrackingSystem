import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { ProjectsService } from 'src/app/services/projects.service';
import { Observable } from 'rxjs';
import { ActivatedRouteSnapshot } from '@angular/router';
import { RouterStateSnapshot } from '@angular/router';
import { Task } from 'src/app/models/task';
import { TasksService } from 'src/app/services/tasks.service';

@Injectable()
export class TaskDetailsResolver implements Resolve<Task> {

    constructor(private service: TasksService) {
    }

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Task> {
        return this.service.getTask(+route.params['id']);
    }
}