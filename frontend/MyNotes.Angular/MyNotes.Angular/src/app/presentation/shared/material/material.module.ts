import { NgModule } from "@angular/core";
import { MatNativeDateModule, MatRippleModule } from "@angular/material/core";
import { MatDialogContent, MatDialogModule } from "@angular/material/dialog";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatSelectModule } from "@angular/material/select";
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatButtonModule } from "@angular/material/button";
import { MatCheckboxModule } from "@angular/material/checkbox";

@NgModule({
    exports:[
        MatDialogModule,
        MatRippleModule,
        MatFormFieldModule,
        MatSelectModule,
        MatAutocompleteModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatDialogContent,
        MatButtonModule,
        MatCheckboxModule
    ],
    imports:[
        MatDialogModule,
        MatRippleModule,
        MatFormFieldModule,
        MatSelectModule,
        MatAutocompleteModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatDialogContent,
        MatButtonModule,
        MatCheckboxModule
    ]
})

export class MaterialModule{

}