import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../../services/auth/authentication.service';
import { AppStore } from '../../../services/store/app.store';
import { Router } from '@angular/router';
import { basePath } from '../../../app-routing.module';

@Component({
  selector: 'app-log-in-telegram-user',
  templateUrl: './log-in-telegram-user.component.html',
  styleUrl: './log-in-telegram-user.component.css'
})
export class LogInTelegramUserComponent implements OnInit {

  constructor(
    private readonly router: Router,
    private readonly appStore: AppStore,
    private readonly authService: AuthenticationService
  ){}

  ngOnInit() {
    this.authService
      .logInWithTelegramUser()
      .subscribe(() => {
        this.appStore.init().then(() => this.router.navigate([basePath]));
      });
  }
}
