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
    isEmailInvalid = false;
    emailErrorMessage = '';
  
    password = '';
    isPasswordInvalid = false;
    passwordErrorMessage = '';
  
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
  }