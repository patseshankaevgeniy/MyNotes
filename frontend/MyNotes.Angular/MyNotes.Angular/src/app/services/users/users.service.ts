import { Injectable } from "@angular/core";
import { UserMapper } from "./user.mapper";
import { MyNotesAPIClient } from "../clients/my-notes-api.client";
import { Observable, map } from "rxjs";
import { User } from "../../models/users/user.model";
import { IUserWithMembers } from "../../models/users/user-with-members.model";
import { ISearchUsersOptions } from "../../models/users/search-users-options.model";

@Injectable({
    providedIn: 'root',
  })
  export class UsersService {
  
    constructor(
      private readonly mapper: UserMapper,
      private readonly myNotesAPIClient: MyNotesAPIClient
    ) {

    }

    getCurrentUser(): Observable<User> {
        return this.myNotesAPIClient
          .getCurrentUser()
          .pipe(map((data) => this.mapper.mapToModel(data.result)));
    }

    searchUsers(options: ISearchUsersOptions): Observable<User[]> {
        return this.myNotesAPIClient
          .getUsers(
            options.hasMembers,
            options.excludeCurrent,
            options.searchPattern,
            options.onlyCurrentUserMembers
          )
          .pipe(map((data) => data.result.map((dto) => this.mapper.mapToModel(dto))));
      }

    getCurrentUserWithMembers(): Promise<IUserWithMembers> {
        return new Promise((resolve) => {
          {
            this.getCurrentUser().subscribe((user) => {
              if (user.hasMembers) {
                this.searchUsers({
                  onlyCurrentUserMembers: true,
                  excludeCurrent: true,
                }).subscribe((userMembers) => {
                  resolve({
                    currentUser: user,
                    members: userMembers,
                  });
                });
              } else {
                resolve({
                  currentUser: user,
                  members: [],
                });
              }
            });
          }
        });
      }
}