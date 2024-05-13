import { Component, HostBinding, OnInit, ViewEncapsulation } from '@angular/core';
import { TelegramAuthCodesService } from '../../../services/telegram-bot/telegram-auth-codes.service';

@Component({
  selector: 'settings-layout',
  host: { 'class': 'settings-layout' },
  templateUrl: './settings-layout.component.html',
  styleUrls: ['./settings-layout.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class SettingsLayoutComponent implements OnInit {

  telegramBotLink: string = '';

  constructor(
    private readonly telegramAuthCodesService: TelegramAuthCodesService
  ) { }

  ngOnInit() {
    this.telegramAuthCodesService
      .getAuthLink()
      .subscribe((link) => (this.telegramBotLink = link));
  }

  onConnectionLinkCliked() {
    window.open(`https://t.me/${this.telegramBotLink}` , '_blank');
  }
}
