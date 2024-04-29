import { NgModule } from "@angular/core";
import { MatNativeDateModule, MatRippleModule } from "@angular/material/core";
import { MatDialogModule } from "@angular/material/dialog";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatSelectModule } from "@angular/material/select";
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatDatepickerModule} from '@angular/material/datepicker';

@NgModule({
    exports:[
        MatDialogModule,
        MatRippleModule,
        MatFormFieldModule,
        MatSelectModule,
        MatAutocompleteModule,
        MatDatepickerModule,
        MatNativeDateModule
    ],
    imports:[
        MatDialogModule,
        MatRippleModule,
        MatFormFieldModule,
        MatSelectModule,
        MatAutocompleteModule,
        MatDatepickerModule,
        MatNativeDateModule
    ]
})

export class MaterialModule{

}