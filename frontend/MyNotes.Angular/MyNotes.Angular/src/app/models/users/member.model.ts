import { MembersItemType } from "../../presentation/pages/members/members-item/members-item-type.model";

export class Member {
  constructor(
    public id: string,
    public userId: string,
    public firstName: string,
    public secondName: string,
    public email: string,
    //public imageUrl: string,
    public type: MembersItemType
  ) { }
}