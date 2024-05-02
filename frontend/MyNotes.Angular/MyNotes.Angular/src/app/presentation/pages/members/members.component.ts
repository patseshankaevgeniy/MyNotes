import { Component, OnInit } from '@angular/core';
import { Member } from '../../../models/users/member.model';
import { MembersService } from '../../../services/users/members.service';
import { MatDialog } from '@angular/material/dialog';
import { CreateMemberPopupComponent } from '../../popups/create-member-popup/create-member-popup.component';
import { MemberPopupResult } from '../../../models/users/member-popup-result.model';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {
  members: Member[] = [];
  addMemberId: string = '';

  constructor(
    private readonly memberService: MembersService,
    private readonly dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.loadMembers();
  }  

  onMemberAcceptCliced(memberId: string) {
    this.memberService
      .acceptMember(memberId)
      .subscribe(() => this.loadMembers());
  }

  onMemberDeclineCliced(memberId: string) {
    this.memberService
      .declineMember(memberId)
      .subscribe(() => this.loadMembers());
  }

  onMemberDeleteliced(memberId: string) {
    this.memberService
      .deleteMember(memberId)
      .subscribe(() => this.loadMembers());
  }

  onAddNewClicked() {
    if (this.members.length >= 1) {
      alert('Сейчас вы можете добавить только одного пользователя');
      return;
    }

    const dialogref =  this.dialog.open(CreateMemberPopupComponent, {
      width: '600px',
      height: '500px',
      data: { userId: this.addMemberId }
    });
    
    dialogref.afterClosed().subscribe(result => {});
    
  }

  private loadMembers() {
    this.memberService
      .getMembers()
      .subscribe((members) => (this.members = members));
  }
}
