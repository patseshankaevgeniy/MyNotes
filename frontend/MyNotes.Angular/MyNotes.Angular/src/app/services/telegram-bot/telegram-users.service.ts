import { EventEmitter, Injectable } from "@angular/core";
import { MyNotesAPIClient } from "../clients/my-notes-api.client";
import { Observable, map } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class TelegramUserService{

    telegramUserStateChanged = new EventEmitter<boolean>();

    constructor(
        private mynotesApiClient: MyNotesAPIClient
    ) { }

    checkTelegramUserExists(): Observable<boolean> {
        return this.mynotesApiClient
          .checkTelegramUserExists()
          .pipe(map(({ result }) => result));
    }

    setTelegramUserExists() {
        this.telegramUserStateChanged.emit(true);
    }

    deleteTelegramUser(): Observable<void> {
        return this.mynotesApiClient
          .deleteTelegramUser()
          .pipe(map((_) => this.telegramUserStateChanged.emit(false)));
    }
}