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
    if (!this.isFormValid()) {
      return;
    }

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

  onKeyDown(event: KeyboardEvent) {
    const regex = /[\p{Alpha}\p{M}\p{Join_C}]/gu;
    const specialKeys: Array<string> = [
      'Backspace',
      'Tab',
      'End',
      'Home',
      'ArrowLeft',
      'ArrowRight',
      'Del',
      'Delete',
    ];

    if (!event.key.match(regex) && !specialKeys.includes(event.key)) {
      event.preventDefault();
      return;
    }
  }

  private isFormValid() {
    var isValid = true;

    const regex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/g; /////////////
    let result = regex.test(this.email);

    if (this.isUserNameInvalid) {
      isValid = false;
    } else if (!this.userName || this.userName == ' ' || this.userName.length === 1) {
      this.userNameErrorMessage = 'Пожалуйста проверьте ваше Имя';
      this.isUserNameInvalid = true;
      isValid = false;
    }

    if (this.isEmailInvalid) {
      isValid = false;
    } else if (!this.email || this.email == ' ' || result == false) {
      this.emailErrorMessage = 'Пожалуйста введите email';
      this.isEmailInvalid = true;
      isValid = false;
    }

    if (this.isPasswordInvalid) {
      isValid = false;
    } else if (!this.password || this.password == ' ' || (this.password.length < 6)) {
      this.passwordErrorMessage = 'Пожалуйста проверьте пароль';
      this.isPasswordInvalid = true;
      isValid = false;
    }
    return isValid;
  }
}
