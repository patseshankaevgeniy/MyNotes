import { SocialLoginModule } from '@abacritt/angularx-social-login';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SignUpComponent } from './sign-up.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    SignUpComponent
  ],
  imports: [
    CommonModule,
    SocialLoginModule,
    FormsModule
  ],
  exports: [
    SignUpComponent
  ],
})
export class SingUpModule { }