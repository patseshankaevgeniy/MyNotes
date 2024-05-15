import { EventEmitter, Injectable } from '@angular/core';
import { User } from '../../models/users/user.model';
import { AccessTokenService } from '../auth/access-token.service';
import { Observable, map } from 'rxjs';
import { UsersService } from '../users/users.service';
import { TelegramUserService } from '../telegram-bot/telegram-users.service';

@Injectable({
  providedIn: 'root',
})
export class AppStore {

  private _currentUser?: User;
  private _usersInGroup: User[] = [];
  private _isTelegramUserExists?: boolean;
  

  constructor(
    private readonly accessTokenService: AccessTokenService,
    private readonly usersService: UsersService,
    private readonly telegramUsersService: TelegramUserService
  ) { }

  currentUserUpdated = new EventEmitter<void>();

  get currentUser(): User {
    return this._currentUser!;
  }

  get isTelegramUserExists(): boolean {
    return this._isTelegramUserExists!;
  }

  reloadCurrentUser() {
     this.usersService.getCurrentUserWithMembers().then((userWithMembers) => {
      this._currentUser = userWithMembers.currentUser;
      this._usersInGroup = userWithMembers.members;

      this.currentUserUpdated.emit();
    });
  }

  get usersInGroup(): User[] {
    var users = [...this._usersInGroup];
    users.push(this.currentUser);
    return users;
  }

  init(): Promise<void> {

    return new Promise((resolve) => {
      // If user not loged in (login or sign up pages)
      if (!this.accessTokenService.isTokenValid()) {
        resolve();
        return;
      }

      // Load user with members and
      this.usersService.getCurrentUserWithMembers().then((userWithMembers) => {
        this._currentUser = userWithMembers.currentUser;
        this._usersInGroup = userWithMembers.members;

        if (this.checkIfLoaded()) {
          resolve();
        }
      });

      // Check is TelegramUser exists
      this.telegramUsersService
        .checkTelegramUserExists()
        .subscribe((isTelegramUserExists) => {
          this._isTelegramUserExists = isTelegramUserExists;

          this.telegramUsersService.telegramUserStateChanged.subscribe(
            (isTelegramUserExists) => {
              this._isTelegramUserExists = isTelegramUserExists;
            }
          );

          if (this.checkIfLoaded()) {
            resolve();
          }
        });

    });
  }
  
 

  private checkIfLoaded(): boolean {
    let usersLoaded = false;
    
    if (this._currentUser) {
      usersLoaded = true;
    }

    return usersLoaded;
  }
}
