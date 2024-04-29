import { NotificationType } from "./notification-type.model";

export class NotificationModel {
  constructor(
    public type: NotificationType,
    public payload: object,
    public userId: string
  ) { }
}