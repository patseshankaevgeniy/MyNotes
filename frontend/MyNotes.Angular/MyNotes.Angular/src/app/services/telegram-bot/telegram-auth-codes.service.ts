import { Injectable } from "@angular/core";
import { MyNotesAPIClient } from "../clients/my-notes-api.client";
import { Observable, map } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class TelegramAuthCodesService {

    constructor(
        private mynotesApiClient: MyNotesAPIClient
    ) { }

    getAuthLink(): Observable<string> {
        return this.mynotesApiClient
          .refreshTelegramAuthCode()
          .pipe(map(({ result }) => result.link!));
    }
}