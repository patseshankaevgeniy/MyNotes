import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable, map, startWith } from 'rxjs';
import { UserNoteService } from '../../../services/user-notes/user-notes-service';
import { NotePriority, UserNoteDto } from '../../../services/clients/my-notes-api.client';
import { UserNoteModel } from '../../../models/user-note-model';

interface Food {
  value: string;
  viewValue: string;
}


@Component({
  selector: 'app-add-user-note',
  templateUrl: './add-user-note.component.html',
  styleUrl: './add-user-note.component.css'
})
export class AddUserNoteComponent {
  public fileTypes = Object.values(NotePriority);
  public noteText: string = '';
  public notePriority!: NotePriority;
  public dateSelected: any;
  public newUserNote!: UserNoteModel;
    
  constructor(
    private dialogRef: MatDialogRef<AddUserNoteComponent>,
    private userNoteService: UserNoteService){}



  submit(){
    const userNote: UserNoteDto = new UserNoteDto ({
      priority: this.notePriority,
      text: this.noteText,
      сompletion: new Date(this.dateSelected)
    })

   this.userNoteService.createUserNote(userNote).subscribe((userNote) => (this.newUserNote = userNote))
    this.dialogRef.close(this.newUserNote);
  }
}


