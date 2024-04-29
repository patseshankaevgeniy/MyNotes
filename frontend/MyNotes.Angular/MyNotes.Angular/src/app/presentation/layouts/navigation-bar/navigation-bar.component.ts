import { Component, OnInit } from "@angular/core";
import { INavigationItem } from "./navigation-bar.model";
import { membersPath, notesPath, settingsPath, signupPath, } from "../../../app-routing.module";
import { NavigationEnd, Router } from "@angular/router";

@Component({
  selector: 'navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.css'],
})

export class NavigationBarComponent implements OnInit {
  
  navigationItems: INavigationItem[] = [
    {
      selected: true,
      name: 'Notes',
      navigationPath: notesPath,
    },
    {
      selected: false,
      name: 'Members',
      navigationPath: membersPath,
    },
    {
      selected: false,
      name: 'Settings',
      navigationPath: settingsPath,
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


  onNavigationItemSelected(navigationItem: INavigationItem) {
    this.navigationItems.forEach(
      (item) => (item.selected = item.name == navigationItem.name)
    );
    this.router.navigateByUrl(navigationItem.navigationPath);
  }
}