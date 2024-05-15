import { Component, HostBinding, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthenticationService } from "../../../services/auth/authentication.service";
import { FailureReason } from "../../../models/login-result.model";


@Component({
    selector: 'log-in',
    templateUrl: './log-in.component.html',
    styleUrls: ['./log-in.component.scss']
  })


  export class LoginComponent implements OnInit {
    @HostBinding('class.log-in') host = true;

    email = '';
    isEmailInvalid = true;
    emailErrorMessage = 'Empty email';
  
    password = '';
    isPasswordInvalid = true;
    passwordErrorMessage = 'Empty password';
  
    isRememberMeChecked = false;

    constructor(
      private router: Router,
      private authService: AuthenticationService
    ) { }

    ngOnInit(): void {
        
    }

    onLoginClicked(){
     

      this.authService
      .logIn(this.email, this.password)
      .subscribe(({ succeeded, failureReason }) => {
        if (!succeeded) {
          switch (failureReason) {
            case FailureReason.userNotFound:
              this.emailErrorMessage = 'Пользователь с таким e-mail не найден';
              this.isEmailInvalid = true;
              break;

            case FailureReason.wrongPassword:
              this.passwordErrorMessage = 'Неверный пароль';
              this.isPasswordInvalid = true;
              break;

            default:
              this.emailErrorMessage = 'Не полчается войти, попробуйте позже';
              this.isEmailInvalid = true;
              break;
          }
        }
      });
    }

    private isFormValid() {
      var isValid = true;
  
      if (this.isEmailInvalid) {
        isValid = false;
      } else if (!this.email || this.email == ' ') {
        this.emailErrorMessage = 'Пожалуйста введите email';
        this.isEmailInvalid = true;
        isValid = false;
      }
  
      if (this.isPasswordInvalid) {
        isValid = false;
      } else if (!this.password || this.password == ' ') {
        this.passwordErrorMessage = 'Пожалуйста введите пароль';
        this.isPasswordInvalid = true;
        isValid = false;
      }
  
      return isValid;
    }
  }