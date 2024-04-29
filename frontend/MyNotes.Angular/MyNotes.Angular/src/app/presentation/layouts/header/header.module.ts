import { RouterLinkWithHref } from "@angular/router";
import { HeaderComponent } from "./header.component";
import { CommonModule, NgStyle } from "@angular/common";
import { NgModule } from "@angular/core";

@NgModule({
    declarations: [
      HeaderComponent
    ],
    imports: [
      RouterLinkWithHref,
      CommonModule,
      NgStyle
    ],
    exports: [HeaderComponent]
  })
  export class HeaderModule { }