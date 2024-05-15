import { NgModule } from "@angular/core";
import { LoginComponent } from "./log-in.component";
import { SocialLoginModule } from '@abacritt/angularx-social-login';
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";

@NgModule({
    declarations: [LoginComponent],
    exports: [LoginComponent],
    imports: [
      SocialLoginModule,
      FormsModule,
      CommonModule 
    ],
  })
  export class LogInModule { }