import { NgModule } from "@angular/core";
import { AppLayoutComponent } from "./layouts/app/app-layout.component";
import { AuthLayoutComponent } from "./shared/auth-layout.component";
import { SettingsLayoutComponent } from "./pages/settings/settings-layout.component";
import { LogInModule } from "./shared/log-in/log-in.module";
import { MembersModule } from "./pages/members/members.module";
import { SingUpModule } from "./shared/sign-up/sign-up.module";
import { NotesModule } from "./pages/notes/notes.module";
import { HeaderModule } from "./layouts/header/header.module";
import { NavigationBarModule } from "./layouts/navigation-bar/navigation-bar.module";
import { AppRoutingModule } from "../app-routing.module";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MaterialModule } from "./shared/material/material.module";
import { CreateMemberPopupComponent } from './popups/create-member-popup/create-member-popup.component';
import { AddUserNotePopupModule } from "./popups/add-user-note/add-user-note.module";


@NgModule({
    declarations: [
        AppLayoutComponent,
        AuthLayoutComponent,
        SettingsLayoutComponent,
        CreateMemberPopupComponent,
    ],
    imports: [
        LogInModule,
        MembersModule,
        SingUpModule,
        NotesModule,
        HeaderModule,
        NavigationBarModule,
        AppRoutingModule,
        BrowserModule,
        MaterialModule,
        ReactiveFormsModule,
        FormsModule,
        AddUserNotePopupModule
        
    ],
    providers: [

    ],
    exports: [
        LogInModule,
        MembersModule,
        SingUpModule,
        NotesModule,
        ReactiveFormsModule,
        MaterialModule,
        FormsModule,
        AddUserNotePopupModule
    ]
})

export class ExpPagesModule{

}