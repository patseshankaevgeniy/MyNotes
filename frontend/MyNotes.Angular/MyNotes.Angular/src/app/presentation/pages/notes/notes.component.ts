import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { UserNoteModel } from "../../../models/user-note-model";
import { UserNoteService } from "../../../services/user-notes/user-notes-service";
import { MatDialog } from "@angular/material/dialog";
import { AddUserNoteComponent } from "../../popups/add-user-note/add-user-note.component";
import { CreateMemberPopupComponent } from "../../popups/create-member-popup/create-member-popup.component";

@Component({
    selector: 'notes',
    host: { 'class': 'notes' },
    templateUrl: './notes.component.html',
    styleUrls: ['./notes.component.css'],
    encapsulation: ViewEncapsulation.None,
})

export class NotesComponent implements OnInit{

    userNotes: UserNoteModel[] = [];

    constructor(
        private readonly userNoteService: UserNoteService,
        private readonly popupRef: MatDialog
    ){}

    ngOnInit(): void {
        this.loadUserNotes();
    }

    private loadUserNotes(){
        this.userNoteService
            .getUserNotes()
            .subscribe((userNotes) => (this.userNotes = userNotes));
    }

   async AddUserNote(): Promise<string>{
      const dialogref =  this.popupRef.open(AddUserNoteComponent, {
            width: '400px',
            height: '500px',
            panelClass: 'popup-container'
        });

        const result = await dialogref.afterClosed().toPromise();
        return result;
    }

    saveChanges(){
        
    }
}