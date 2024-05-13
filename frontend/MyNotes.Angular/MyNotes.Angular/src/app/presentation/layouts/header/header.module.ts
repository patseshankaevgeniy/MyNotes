import { RouterLinkWithHref } from "@angular/router";
import { HeaderComponent } from "./header.component";
import { CommonModule, NgStyle } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";

@NgModule({
    declarations: [
      HeaderComponent
    ],
    imports: [
      RouterLinkWithHref,
      CommonModule,
      NgStyle,
      FormsModule
    ],
    exports: [HeaderComponent]
  })
  export class HeaderModule { }