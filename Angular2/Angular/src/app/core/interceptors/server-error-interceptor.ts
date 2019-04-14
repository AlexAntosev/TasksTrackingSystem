import { Injectable } from '@angular/core';
import { 
  HttpEvent, HttpRequest, HttpHandler, 
  HttpInterceptor, HttpErrorResponse 
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { ErrorService } from 'src/app/services/error.service';

@Injectable()
export class ServerErrorInterceptor implements HttpInterceptor {

    constructor(private errorService: ErrorService) {

    }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {            
          // refresh token
        }
        return this.errorService.handleError(error);
      })
    );    
  }
}