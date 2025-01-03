import { MasterBaseViewModel } from "../MasterBaseViewModel";

export class UserViewModel extends MasterBaseViewModel {
    public firstName!: string;
    public lastName!: string;
    public email!: string;
    public userName!: string;
    public phoneNumber!: string;
    public avatar!: string;
    //public dateOfBirth!: Date;
    public password!: string;
    public confirmPassword!: string;
}