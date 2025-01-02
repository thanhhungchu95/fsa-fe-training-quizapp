import QuestionTypeEnum from "../../core/enums/QuestionTypeEnum";
import ISearchQuery from "./ISearchQuery";

export default interface ISearchQuestionQuery extends ISearchQuery {
    content?: string;
    questionType?: QuestionTypeEnum;
}