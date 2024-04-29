import { Component, OnInit } from "@angular/core";
import { IHeaderNavigationItem } from "./header.model";
import { NavigationEnd, Router } from "@angular/router";

@Component({
    selector: 'header-panel',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css'],
  })
  
  export class HeaderComponent implements OnInit {
    
    navigationItems: IHeaderNavigationItem[] = [
      {
        selected: true,
        name: 'Pricing',
        navigationPath: '',
      },
      {
        selected: false,
        name: 'Reviews',
        navigationPath: '',
      },
      {
        selected: false,
        name: 'Contact',
        navigationPath: '',
      },
      {
        selected: false,
        name: 'About',
        navigationPath: '',
      },
      {
        selected: false,
        name: 'Features',
        navigationPath: '',
      },
    ];

    constructor(
      private readonly router: Router,
    ){
      router.events.subscribe(event => {
        if (event instanceof NavigationEnd) {
          const currentUrl = this.navigationItems.find(t => t.selected)?.name;
          if (currentUrl && !this.router.url.includes(currentUrl)) {
            this.navigationItems.forEach(
              (item) => (item.selected = this.router.url.includes(item.navigationPath))
            );
          }
        }
      });
    }

    ngOnInit(): void {
      this.navigationItems.forEach(
        (item) => (item.selected = this.router.url.includes(item.navigationPath))
      );
    }

    onNavigationItemSelected(navigationItem: IHeaderNavigationItem) {
      this.navigationItems.forEach(
        (item) => (item.selected = item.name == navigationItem.name)
      );
      this.router.navigateByUrl(navigationItem.navigationPath);
    }
  }