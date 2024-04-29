import { EventEmitter, Injectable } from '@angular/core';
import { User } from '../../models/users/user.model';
import { AccessTokenService } from '../auth/access-token.service';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AppStore {

  private _currentUser?: User;
  private _usersInGroup: User[] = [];
  

  constructor(
    private readonly accessTokenService: AccessTokenService,
  ) { }

  currentUserUpdated = new EventEmitter<void>();

  get currentUser(): User {
    return this._currentUser!;
  }

  reloadCurrentUser() {
    
  }

  get usersInGroup(): User[] {
    var users = [...this._usersInGroup];
    users.push(this.currentUser);
    return users;
  }

 

  private checkIfLoaded(): boolean {
    let usersLoaded = false;
    
    
   

    if (this._currentUser) {
      usersLoaded = true;
    }

   

    return usersLoaded;
  }
}
