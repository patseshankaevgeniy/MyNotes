import { Component, OnDestroy, OnInit, ViewEncapsulation } from "@angular/core";
import { UserNoteModel } from "../../../models/user-note-model";
import { UserNoteService } from "../../../services/user-notes/user-notes-service";
import { MatDialog } from "@angular/material/dialog";
import { AddUserNoteComponent } from "../../popups/add-user-note/add-user-note.component";
import { NotificationService } from "../../../services/notifications/notification.service";
import { Subscription } from "rxjs";
import { NotificationType } from "../../../services/notifications/notification-type.model";

@Component({
    selector: 'notes',
    host: { 'class': 'notes' },
    templateUrl: './notes.component.html',
    styleUrls: ['./notes.component.css'],
    encapsulation: ViewEncapsulation.None,
})

export class NotesComponent implements OnInit, OnDestroy{

    private notesUpdatedSubscription?: Subscription;
    userNotes: UserNoteModel[] = [];

    constructor(
        private readonly userNoteService: UserNoteService,
        private readonly popupRef: MatDialog,
        private readonly notificationService: NotificationService
    ){}
    ngOnDestroy(): void {
        this.notesUpdatedSubscription?.unsubscribe();
    }

    ngOnInit(): void {
        this.loadUserNotes();
        this.subscribeOnPurchaseUpdated();
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

    changeActual(userNote: UserNoteModel, event: any){
        this.userNoteService.deleteUserNote(userNote.id).subscribe();
    }

    saveChanges(){
        this.ngOnInit();
    }

   private subscribeOnPurchaseUpdated(){
        this.notesUpdatedSubscription = this.notificationService
        .messageReceived
        .subscribe(notification => {
            if (notification.type == NotificationType.userNotesUpdate) {
            this.loadUserNotes();
            }
        });
    }
}