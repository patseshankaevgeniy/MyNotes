import { Component, HostBinding, OnInit, ViewEncapsulation } from '@angular/core';
import { TelegramAuthCodesService } from '../../../services/telegram-bot/telegram-auth-codes.service';
import { AppStore } from '../../../services/store/app.store';
import { NotificationService } from '../../../services/notifications/notification.service';
import { NotificationType } from '../../../services/notifications/notification-type.model';
import { TelegramUserService } from '../../../services/telegram-bot/telegram-users.service';

@Component({
  selector: 'settings-layout',
  host: { 'class': 'settings-layout' },
  templateUrl: './settings-layout.component.html',
  styleUrls: ['./settings-layout.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class SettingsLayoutComponent implements OnInit {

  telegramBotLink: string = '';
  isTelegramUserConected: boolean = false;

  constructor(
    private readonly telegramAuthCodesService: TelegramAuthCodesService,
    private readonly appStore: AppStore,
    private readonly notificationService:NotificationService,
    private readonly telegramUsersService: TelegramUserService
  ) { }

  ngOnInit() {
    this.isTelegramUserConected = this.appStore.isTelegramUserExists;

    this.telegramAuthCodesService
      .getAuthLink()
      .subscribe((link) => (this.telegramBotLink = link));

      this.notificationService.messageReceived.subscribe((notification) => {
        if (notification.type == NotificationType.telegramBotConnected) {
          this.telegramUsersService.setTelegramUserExists();
          this.isTelegramUserConected = true;
        }
      });
  }

  onConnectionLinkCliked() {
    window.open(this.telegramBotLink , '_blank');
  }

  onTelegramUserDeleted(){
    this.isTelegramUserConected = false;
  }
}
