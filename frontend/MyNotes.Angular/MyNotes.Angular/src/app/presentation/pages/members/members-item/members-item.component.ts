import { Component, EventEmitter, Input, Output } from "@angular/core";
import { MembersItemType } from "./members-item-type.model";

@Component({
  selector: 'members-item',
  templateUrl: './members-item.component.html',
  styleUrls: ['./members-item.component.scss']
})

export class MembersItemComponent {
  @Input() memberId?: string;
  @Input() firstName?: string;
  @Input() secondName?: string;
  @Input() email?: string;
  //@Input() imageSrc?: string;
  @Input() type?: MembersItemType;

  @Output() acceptClick = new EventEmitter<string>();
  @Output() declineClick = new EventEmitter<string>();
  @Output() deleteClick = new EventEmitter<string>();

  onAcceptClick() {
    this.acceptClick.emit(this.memberId);
  }

  onDeclineClick() {
    this.declineClick.emit(this.memberId);
  }

  onDeleteClick() {
    this.deleteClick.emit(this.memberId);
  }
}