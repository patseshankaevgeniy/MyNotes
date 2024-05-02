import { Injectable } from "@angular/core";
import { CreateMemberDto, IMemberDto, MemberStatus, MyNotesAPIClient } from "../clients/my-notes-api.client";
import { Member } from "../../models/users/member.model";
import { Observable, map } from "rxjs";
import { MembersItemType } from "../../presentation/pages/members/members-item/members-item-type.model";

@Injectable({
    providedIn: 'root'
})
export class MembersService {

    constructor(
        private readonly myNotesAPIClient: MyNotesAPIClient
    ) { }

    getMembers(): Observable<Member[]> {
        return this.myNotesAPIClient
          .getMembers()
          .pipe(map(({ result }) => result.map(this.mapToModel)));
    }

    createMember(userId: string): Observable<void> {
        return this.myNotesAPIClient
          .createMember(new CreateMemberDto({ userId: userId }))
          .pipe(map(_ => { }));
    }

    acceptMember(memberId: string): Observable<void> {
        return this.myNotesAPIClient
          .acceptInvitation()
          .pipe(map(_ => { }));
    }

    declineMember(memberId: string): Observable<void> {
        return this.myNotesAPIClient
          .deleteMember(memberId)
          .pipe(map(_ => { }));
    }

    deleteMember(memberId: string): Observable<void> {
        return this.myNotesAPIClient
          .deleteMember(memberId)
          .pipe(map(_ => { }));
    }

    private mapToModel(dto: IMemberDto): Member {

        let mapStatus = (status: MemberStatus) => {
          switch (status) {
            case MemberStatus.Approved: return MembersItemType.approved;
            case MemberStatus.WaitForApproval: return MembersItemType.waitForApproval;
            case MemberStatus.RequiredApproval: return MembersItemType.requiredApproval;
            default: throw new Error();
          }
        };
    
        return new Member(
          dto.id!,
          dto.userId!,
          dto.firstName!,
          dto.secondName!,
          dto.email!,
          mapStatus(dto.status!)
        );
    }
}  