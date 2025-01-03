import QuestionTypeEnum from "../../core/enums/QuestionTypeEnum";
import IBaseDetailsModel from "../IBaseDetailsModel";

export default interface IQuestionDetailsModel extends IBaseDetailsModel {
    content: string;
    questionType: QuestionTypeEnum;
    isActive: boolean;
}