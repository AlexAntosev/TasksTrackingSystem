import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationService } from 'src/app/services/notification.service';
import { AlertService } from 'src/app/services/alert.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor(private router: Router, private notificationService: NotificationService, private alertService: AlertService) {
  }

  public handleError = (error: HttpErrorResponse): Observable<any> => {
    if(error.error != null) {
      this.alertService.error(error.error.Message);
      return throwError(error.error);
    }
    else{
      this.alertService.error(error.message);
      return throwError(error);
    }
  }
}
