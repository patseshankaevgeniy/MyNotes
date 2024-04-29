export class LoginResult {
  constructor(
    public succeeded: boolean,
    public failureReason: FailureReason
  ) { }
}

export enum FailureReason {
  userNotFound,
  wrongPassword,
  unknownReason,
  existingUser
}