import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationService } from 'src/app/services/notification.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor(private router: Router, private notificationService: NotificationService) {
  }
  public handleError = (error: HttpErrorResponse): Observable<any> => {
    
    
    this.notificationService.showError('Error: ' + error.error.Message);
    return throwError(error);
  }
}
