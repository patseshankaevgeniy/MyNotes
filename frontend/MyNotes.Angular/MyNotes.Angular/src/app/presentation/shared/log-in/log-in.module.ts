import { NgModule } from "@angular/core";
import { LoginComponent } from "./log-in.component";
import { SocialLoginModule } from '@abacritt/angularx-social-login';
import { FormsModule } from "@angular/forms";

@NgModule({
    declarations: [LoginComponent],
    exports: [LoginComponent],
    imports: [
      SocialLoginModule,
      FormsModule
    ],
  })
  export class LogInModule { }