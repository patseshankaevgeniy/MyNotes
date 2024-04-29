import { CommonModule, NgStyle } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterLinkWithHref } from '@angular/router';
import { NavigationBarComponent } from './navigation-bar.component';

@NgModule({
  declarations: [
    NavigationBarComponent
  ],
  imports: [
    RouterLinkWithHref,
    CommonModule,
    NgStyle
  ],
  exports: [NavigationBarComponent]
})
export class NavigationBarModule { }