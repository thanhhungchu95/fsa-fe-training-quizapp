import ILoginModel from "./ILoginModel";

export default interface IRegisterModel extends ILoginModel{
    firstName: string;
    lastName: string;
    email: string;
    confirmPassword: string;
    phoneNumber: string;
    dateOfBirth: Date;
}