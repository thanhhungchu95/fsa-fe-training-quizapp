import IBaseDetailsModel from "../IBaseDetailsModel";

export default interface IQuizDetailsModel extends IBaseDetailsModel {
    title: string;
    description: string;
    questionIdWithOrders: any[];
    duration: number;
    isActive: boolean;
    thumbnailUrl: string;
}