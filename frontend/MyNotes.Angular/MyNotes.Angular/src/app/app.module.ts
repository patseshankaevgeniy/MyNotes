import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppStore } from './services/store/app.store';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { MYNOTES_API_BASE_URL, MyNotesAPIClient } from './services/clients/my-notes-api.client';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AccessTokenInterceptor } from './services/auth/access-token.interceptor';
import { environment } from '../environments/environment.prod';
import { ExpPagesModule } from './presentation/presentation.module';
import { NgxSkeletonLoaderModule } from 'ngx-skeleton-loader';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';
import { FormsModule } from '@angular/forms';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatRippleModule } from '@angular/material/core';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ExpPagesModule,
    HttpClientModule,
    SocialLoginModule,
    FormsModule,
    NgxSkeletonLoaderModule.forRoot(),
    JwtModule.forRoot({
      config: {
          tokenGetter: () => localStorage.getItem('access_token'),
      },
  }),
  ],
  providers: [
    AppStore,
    
    JwtHelperService,
    MyNotesAPIClient,
    {
      provide: HTTP_INTERCEPTORS,
      multi: true,
      useClass: AccessTokenInterceptor,
    },
    {
      provide: MYNOTES_API_BASE_URL,
      useValue: environment.myNotesApiClientBaseUrl,
    },
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
          autoLogin: false,
          providers: [
              {
                  id: GoogleLoginProvider.PROVIDER_ID,
                  provider: new GoogleLoginProvider(environment.googleClientId, {
                      oneTapEnabled: false,
                  }),
              },
          ],
      } as SocialAuthServiceConfig,
  },
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
