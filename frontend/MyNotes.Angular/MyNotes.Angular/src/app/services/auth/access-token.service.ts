import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AccessTokenService {

  constructor(
    private readonly jwtHelperService: JwtHelperService
  ) { }

  isTokenValid() {
    return !this.jwtHelperService.isTokenExpired();
  }

  setToken(accessToken: string) {
    if (!accessToken) {
      throw new Error("Can't set empty token");
    }

    localStorage.setItem('access_token', accessToken);
  }

  getToken() {
    return this.jwtHelperService.tokenGetter();
  }
}