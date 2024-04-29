import { NgModule } from "@angular/core";
import { NotesComponent } from "./notes.component";
import { BrowserModule } from "@angular/platform-browser";

@NgModule ({
    declarations: [
        NotesComponent
    ],
    imports: [
        BrowserModule
    ],
    exports: [
        NotesComponent
    ]
})

export class NotesModule {

}