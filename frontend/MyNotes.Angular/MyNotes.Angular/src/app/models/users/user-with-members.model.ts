import { User } from "./user.model";

export interface IUserWithMembers {
  currentUser: User,
  members: User[]
}