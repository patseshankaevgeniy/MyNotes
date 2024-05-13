import { Component, HostBinding, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../../services/auth/authentication.service';
import { FailureReason } from '../../../models/login-result.model';
import { loginPath } from '../../../app-routing.module';

@Component({
  selector: 'sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class SignUpComponent {
  @HostBinding('class.sign-up') host = true;
  
  userName = '';
  isUserNameInvalid = false;
  userNameErrorMessage = '';

  email = '';
  isEmailInvalid = false;
  emailErrorMessage = '';

  password = '';
  isPasswordInvalid = false;
  passwordErrorMessage = '';


  constructor(
    private router: Router,
    private authService: AuthenticationService
  ) { }



  onUserNameChanged(firstName: string) {
    this.userName = firstName;
  }

  onEmailChanged(email: string) {
    this.email = email;
  }

  onPasswordChanged(password: string) {
    this.password = password;
    console.log(this.password)
  }

  onRegistrationClicked() {
    this.authService
      .signUp( this.email, this.password, this.userName)
      .subscribe(({ succeeded, failureReason }) => {
        if (!succeeded) {
          switch (failureReason) {
            case FailureReason.existingUser:
              this.emailErrorMessage = 'Пользователь с таким e-mail уже существует';
              this.isEmailInvalid = true;
              break;

            default:
              this.emailErrorMessage = 'Не полчается зарегистрироваться, попробуйте позже';
              this.isEmailInvalid = true;
              break;
          }
        }
      });
  }

  onLogInAccountClicked() {
    this.router.navigate([loginPath])
  }
}
