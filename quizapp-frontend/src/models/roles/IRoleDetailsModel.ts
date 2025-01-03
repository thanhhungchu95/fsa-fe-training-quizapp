import IBaseDetailsModel from "../IBaseDetailsModel";

export default interface IRoleDetailsModel extends IBaseDetailsModel {
    name: string;
    description: string;
    isActive: boolean;
}