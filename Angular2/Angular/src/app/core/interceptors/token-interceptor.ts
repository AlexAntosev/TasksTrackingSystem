import { Injectable } from "@angular/core";
import { HttpInterceptor } from '@angular/common/http';
import { HttpRequest } from '@angular/common/http';
import { HttpHandler } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { HttpEvent } from '@angular/common/http';
import { TokenService } from 'src/app/services/token.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    constructor(private tokenService: TokenService) {

    }

    intercept(request: HttpRequest<any>, handler: HttpHandler): Observable<HttpEvent<any>> {
        
        request = request.clone({
            setHeaders: {
                Authorization: `Bearer ${this.tokenService.fetchToken().signature}`
            }
        })

        return handler.handle(request);
    }
}