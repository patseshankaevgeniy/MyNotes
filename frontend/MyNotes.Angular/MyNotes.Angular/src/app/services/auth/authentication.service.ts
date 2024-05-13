import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {
  LogInDto,
  MyNotesAPIClient,
  SignUpDto,
  TelegramUserLogInDto
} from '../clients/my-notes-api.client';
import { basePath, loginPath } from '../../app-routing.module';
import { AccessTokenService } from './access-token.service';
import { AppStore } from '../store/app.store';
import { Observable, Subject, map } from 'rxjs';
import { LoginResult } from '../../models/login-result.model';
import { TelegramWebAppService } from '../telegram-bot/telegram-web-app.service';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private extAuthChangeSub = new Subject<SocialUser>();

  constructor(
    private readonly router: Router,
    private readonly appStore: AppStore,
    private readonly mynotesAPIClient: MyNotesAPIClient,
    private readonly socialAuthService: SocialAuthService,
    private readonly accessTokenService: AccessTokenService,
    private readonly telegramWebAppService: TelegramWebAppService
  ) {
    this.socialAuthService.authState.subscribe((user: SocialUser) =>
      this.extAuthChangeSub.next(user)
    );
  }

  get isAuthenticated(): boolean {
    return this.accessTokenService.isTokenValid();
  }

  logIn(email: string, password: string): Observable<LoginResult> {
    return this.mynotesAPIClient
      .logIn(new LogInDto({ email: email, password: password }))
      .pipe(
        map(({ result }) => {
          if (result.succeeded) {
            this.accessTokenService.setToken(result.accessToken!);
            this.router.navigate([basePath]);
          }
          return new LoginResult(result.succeeded!, result.failureReason!);
        })
      );
  }

  signOut() {
    this.socialAuthService.signOut();
    localStorage.removeItem('access_token');
    this.router.navigate([loginPath]);
  }

  signUp(userName: string, email: string, password: string): Observable<LoginResult> {
    const newUser = new SignUpDto({  email, userName, password })
    return this.mynotesAPIClient  
      .signUp(newUser)
      .pipe(
        map(({ result }) => {
          if (result.succeeded) {
            this.accessTokenService.setToken(result.accessToken!);
            this.appStore.init().then(() => this.router.navigate([basePath]));
          }
          return new LoginResult(result.succeeded!, result.failureReason!);
        })
      );
  }

  checkCanSignInWitTelegramUser(): boolean {
    return this.telegramWebAppService.isActive;
  }

  logInWithTelegramUser(): Observable<void> {
    const telegramUserId = this.telegramWebAppService.getTelegramUserId;
    return this.mynotesAPIClient
      .logInWithTelegramUser(new TelegramUserLogInDto({ telegramUserId: telegramUserId! }))
      .pipe(map(({ result }) => {
        this.accessTokenService.setToken(result.accessToken!);
    }));
  }
}