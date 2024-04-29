import { CommonModule, NgStyle } from "@angular/common";
import { NgModule } from "@angular/core";
import { MembersItemComponent } from "./members-item/members-item.component";
import { MembersComponent } from "./members.component";

@NgModule({
    declarations: [
        MembersComponent,
        MembersItemComponent,
    ],
    exports: [
        MembersItemComponent,
        MembersComponent
    ],
    imports: [
        NgStyle,
        CommonModule
    ]
})
export class MembersModule { }