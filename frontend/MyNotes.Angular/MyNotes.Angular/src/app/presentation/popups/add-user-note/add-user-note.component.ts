import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserNoteService } from '../../../services/user-notes/user-notes-service';
import { NotePriority, UserNoteDto } from '../../../services/clients/my-notes-api.client';
import { UserNoteModel } from '../../../models/user-note-model';
import { FormControl, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}


@Component({
  selector: 'app-add-user-note',
  templateUrl: './add-user-note.component.html',
  styleUrl: './add-user-note.component.scss'
})
export class AddUserNoteComponent {


  emailFormControl = new FormControl('', [Validators.required]);

  matcher = new MyErrorStateMatcher();
  

  checkWithoutDate: boolean = false;
  // public fileTypes = Object.values(NotePriority);
  // public noteText: string = '';
  // public notePriority!: NotePriority;
  // public dateSelected: any;
  // public newUserNote!: UserNoteModel;
    
  // constructor(
  //   private dialogRef: MatDialogRef<AddUserNoteComponent>,
  //   private userNoteService: UserNoteService){}



  // submit(){
  //   const userNote: UserNoteDto = new UserNoteDto ({
  //     priority: this.notePriority,
  //     text: this.noteText,
  //     сompletion: new Date(this.dateSelected)
  //   })

  //  this.userNoteService.createUserNote(userNote).subscribe((userNote) => (this.newUserNote = userNote))
  //   this.dialogRef.close(this.newUserNote);
  // }
}


