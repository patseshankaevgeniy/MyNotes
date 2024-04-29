import { Component, HostBinding, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'settings-layout',
  host: { 'class': 'settings-layout' },
  templateUrl: './settings-layout.component.html',
  styleUrls: ['./settings-layout.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class SettingsLayoutComponent { }
