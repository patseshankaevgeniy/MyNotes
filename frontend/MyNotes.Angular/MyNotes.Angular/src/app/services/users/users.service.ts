import { Injectable } from "@angular/core";
import { UserMapper } from "./user.mapper";
import { MyNotesAPIClient } from "../clients/my-notes-api.client";

@Injectable({
    providedIn: 'root',
  })
  export class UsersService {
  
    constructor(
      private readonly mapper: UserMapper,
      private readonly myNotesAPIClient: MyNotesAPIClient
    ) { }
}