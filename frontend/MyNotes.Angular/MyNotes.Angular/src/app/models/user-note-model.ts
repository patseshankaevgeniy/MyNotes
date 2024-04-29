import { NotePriority } from "../services/clients/my-notes-api.client";

export class UserNoteModel{
    constructor(
        public id: string,
        public text: string,
        public created: Date,
        public сompletion: Date,
        public userId: string,
        public priority: NotePriority,
        public isActual: boolean
    ){}
}