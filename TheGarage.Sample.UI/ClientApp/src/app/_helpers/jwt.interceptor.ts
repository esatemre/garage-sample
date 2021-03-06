import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthService } from '../_services';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add authorization header with jwt token if available
    let currentToken = this.authenticationService.currentTokenValue;
    if (currentToken && currentToken.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentToken.token}`
        }
      });
    }

    return next.handle(request);
  }
}
