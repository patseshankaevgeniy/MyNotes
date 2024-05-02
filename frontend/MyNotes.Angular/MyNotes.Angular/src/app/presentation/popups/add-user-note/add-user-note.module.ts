import { NgModule } from "@angular/core";
import { AddUserNoteComponent } from "./add-user-note.component";
import { MaterialModule } from "../../shared/material/material.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatFormFieldModule } from "@angular/material/form-field";
import {MatInputModule} from '@angular/material/input';




@NgModule({
    declarations: [
        AddUserNoteComponent
    ],
    imports: [
        FormsModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule
    ],
    
    exports: [

    ]
})
export class AddUserNotePopupModule{}