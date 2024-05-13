import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { IUserNoteDto, MyNotesAPIClient, UserNoteDto } from "../clients/my-notes-api.client";
import { UserNoteModel } from "../../models/user-note-model";

@Injectable({
    providedIn: 'root',
})

export class UserNoteService{
    constructor(
        private userNoteApiclient: MyNotesAPIClient
    ) { }

    getUserNotes(): Observable<UserNoteModel[]>{
        return this.userNoteApiclient
            .getUserNotes()
            .pipe(map(({result}) => result.map(userNote => this.mapToModel(userNote))));
    }

    createUserNote(userNote: UserNoteDto): Observable<UserNoteModel>{
        return this.userNoteApiclient
            .createUserNote(userNote)
            .pipe(map(({ result }) => this.mapToModel(result)));
    }

    deleteUserNote(userNoteId: string): Observable<void>{
        return this.userNoteApiclient
            .deleteUserNote(userNoteId)
            .pipe(map(_ => { }));
    }

    

    private mapToModel(dto: IUserNoteDto): UserNoteModel {
        return new UserNoteModel(
          dto.id!,
          dto.text!,
          dto.created!,
          dto.сompletion!,
          dto.userId!,
          dto.priority!,
          dto.isActual!
        );
    }

    private mapToDto(model: UserNoteModel): IUserNoteDto{
        return{

            id: model.id,
            text: model.text,
            created: model.created,
            сompletion: model.сompletion,
            userId: model.userId,
            priority: model.priority,
            isActual: model.isActual
        }
    }
}