import { MasterBaseViewModel } from "../MasterBaseViewModel";

export class QuizViewModel extends MasterBaseViewModel {
    public title!: string;
    public description!: string;
    public duration!: number;
    public thumbnailUrl!: string;
    public numberOfQuestions!: number;
}