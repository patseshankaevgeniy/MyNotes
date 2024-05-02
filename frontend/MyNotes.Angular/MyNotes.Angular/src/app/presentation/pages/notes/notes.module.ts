import { NgModule } from "@angular/core";
import { NotesComponent } from "./notes.component";
import { BrowserModule } from "@angular/platform-browser";
import {
    MatDialog,
    MatDialogRef,
    MatDialogActions,
    MatDialogClose,
    MatDialogTitle,
    MatDialogContent,
  } from '@angular/material/dialog';
  import {MatButtonModule} from '@angular/material/button';

@NgModule ({
    declarations: [
        NotesComponent
    ],
    imports: [
        BrowserModule,
        MatButtonModule, MatDialogActions, MatDialogClose, MatDialogTitle, MatDialogContent
    ],
    exports: [
        NotesComponent
    ]
})

export class NotesModule {

}