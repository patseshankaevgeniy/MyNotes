import { Component } from '@angular/core';

@Component({
  selector: 'app-layout',
  template: `
    <div class="navigation-bar">
      <navigation-bar></navigation-bar>
    </div>

    <div class="router-outlet">
      <router-outlet></router-outlet>
    </div>
  `,
  styles: [
    `

      :host {
        display: grid;
        grid-template-areas: 'navigation-bar router-outlet';
        grid-template-rows: 100vh;
        grid-template-columns: 218px 1fr;
      }
    `,
  ],
})
export class AppLayoutComponent {}