export class User {
  constructor(
    public userId: string,
    public hasMembers: boolean,
    public firstName: string,
    public secondName: string,
    public email: string,
    //public imageId: string,
   // public imageUrl: string,
    public languageId: string
  ) { }
}