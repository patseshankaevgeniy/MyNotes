import { SocialLoginModule } from '@abacritt/angularx-social-login';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SignUpComponent } from './sign-up.component';

@NgModule({
  declarations: [
    SignUpComponent
  ],
  imports: [
    CommonModule,
    SocialLoginModule
  ],
  exports: [
    SignUpComponent
  ],
})
export class SingUpModule { }