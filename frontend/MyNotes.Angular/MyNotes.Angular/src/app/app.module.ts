import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavigationBarModule } from './navigation-bar/navigation-bar.module';
import { AppLayoutComponent } from './pages/app-layout.component';
import { SignInComponent } from './pages/auth/sign-in/sign-in.component';

@NgModule({
  declarations: [
    AppComponent,
    AppLayoutComponent,
    SignInComponent
  ],
  imports: [
    BrowserModule,
    NavigationBarModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
