import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationService } from './authentication.service';
import { AccessTokenService } from './access-token.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AccessTokenInterceptor implements HttpInterceptor {
  
  constructor(
    private readonly accessTokenService: AccessTokenService,
    private readonly authenticationService: AuthenticationService
  ) { }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (this.authenticationService.isAuthenticated) {
      req = req.clone({
        setHeaders: {
          Authorization: 'Bearer ' + this.accessTokenService.getToken(),
        },
      });
    }
    return next.handle(req);
  }
}