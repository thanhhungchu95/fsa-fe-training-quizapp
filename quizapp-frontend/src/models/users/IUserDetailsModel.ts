import IBaseDetailsModel from "../IBaseDetailsModel";

export default interface IUserDetailsModel extends IBaseDetailsModel {
    firstName: string;
    lastName: string;
    email: string;
    userName: string;
    phoneNumber: string;
    isActive: boolean;
    avatar: string;
    //dateOfBirth: Date;
    password?: string;
    confirmPassword?: string;
}
