import QuestionTypeEnum from "../../core/enums/QuestionTypeEnum";
import { MasterBaseViewModel } from "../MasterBaseViewModel";

export class QuestionViewModel extends MasterBaseViewModel {
    public content!: string;
    public numberOfAnswers!: number;
    public questionType!: QuestionTypeEnum;
}