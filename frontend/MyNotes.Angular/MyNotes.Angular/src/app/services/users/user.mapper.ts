import { Injectable } from "@angular/core";
import { User } from "../../models/users/user.model";
import { IUserDto, UserDto } from "../clients/my-notes-api.client";

@Injectable({
  providedIn: 'root',
})
export class UserMapper {

  mapToModel(dto: IUserDto): User {
    return new User(
      dto.userId!,
      dto.hasMembers!,
      dto.firstName!,
      dto.secondName!,
      dto.email!,
      dto.languageId!
    );
  }

  mapToDto(model: User): UserDto {
    return new UserDto({
      userId: model.userId,
      hasMembers: model.hasMembers,
      firstName: model.firstName!,
      secondName: model.secondName!,
      email: model.email!,
      languageId: model.languageId
    });
  }
}