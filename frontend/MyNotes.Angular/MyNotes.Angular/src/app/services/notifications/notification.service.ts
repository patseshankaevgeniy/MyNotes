import { EventEmitter, Injectable } from "@angular/core";
import { NotificationModel } from "./notification.model";
import * as signalR from "@microsoft/signalr";
import { AppStore } from "../store/app.store";
import { environment } from "../../../environments/environment";
import { NotificationType } from "./notification-type.model";

@Injectable({
    providedIn: 'root'
})

export class NotificationService {

    connectionEstablished = new EventEmitter<Boolean>();
    messageReceived = new EventEmitter<NotificationModel>();

    private connectionIsEstablished = false;
    private _hubConnection!: signalR.HubConnection;

    constructor(
        private appStore: AppStore
    ){
      this.createConnection();
      this.subscribeOnNotifications();
      this.startConnection();
    }

    private createConnection() {
        this._hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(environment.myNotesApiClientBaseUrl + environment.signalR.urlPath)
            .build();
    }

    private startConnection(): void {
        this._hubConnection
          .start()
          .then(() => {
            this.connectionIsEstablished = true;
            console.log('Hub connection started');
            this.connectionEstablished.emit(true);
          })
          .catch(err => {
            console.log('Error while establishing connection, retrying...');
            setTimeout(this.startConnection, 5000);
          });
    }

    private subscribeOnNotifications(): void {
        this._hubConnection.on(
          environment.signalR.notificationMethodName,
          notification => this.handleNotification(notification));
    }

    private handleNotification(notification: NotificationModel) {
        if (this.appStore.usersInGroup.some(x => x.userId == notification.userId)) {
          console.log("Notification was recieved: " + NotificationType[notification.type]);
          this.messageReceived.emit(notification);
        }
    }
}