import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MemberPopupResult } from '../../../models/users/member-popup-result.model';
import { IMember } from '../../../models/users/add-member-popup.model';
import { UsersService } from '../../../services/users/users.service';
import { User } from '../../../models/users/user.model';
import { MembersService } from '../../../services/users/members.service';

@Component({
  selector: 'app-create-member-popup',
  templateUrl: './create-member-popup.component.html',
  styleUrl: './create-member-popup.component.scss'
})
export class CreateMemberPopupComponent implements OnInit {
  members: IMember[] = [];
  searchText: string = '';
  addButtonDisabled: boolean = true;

  constructor(
    private readonly userInfoService: UsersService,
    private readonly membersService: MembersService,
    public dialogRef: MatDialogRef<CreateMemberPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: MemberPopupResult,
  ){}

 ngOnInit(): void {
  this.addButtonDisabled = true;
  this.userInfoService
    .searchUsers({
      hasMembers: false,
      excludeCurrent: true,
    })
    .subscribe(
      (users) => (this.members = users.map((user) => this.mapToMember(user)))
    );
 }

 onSearchChanged(text: string) {
  this.addButtonDisabled = true;
  this.userInfoService
    .searchUsers({
      searchPattern: text,
      hasMembers: false,
      excludeCurrent: true,
    })
    .subscribe(
      (users) => (this.members = users.map((user) => this.mapToMember(user)))
    );
}

onMemberClick(userId: string) {
  if(this.addButtonDisabled){
    this.addButtonDisabled = !this.addButtonDisabled
  }
  var member = this.members.find((m) => m.userId == userId)!;
  member.selected = !member.selected;
  this.addButtonDisabled = !member.selected;
}


onAddClicked() {
  var member = this.members.find((m) => m.selected)!;
  this.membersService.createMember(member.userId).subscribe()
  this.dialogRef.close({ userId: member.userId });
}

 private mapToMember(user: User): IMember {
  return {
    userId: user.userId,
    firstName: user.firstName!,
    secondName: user.secondName!,
    email: user.email!,
    selected: false,
  };
}
}
