import IQuizItem from "../../models/IQuizItem";
import { FormatHelper } from "../../helpers/FormatHelper";

const QuizCard = ({ key, title, duration, description, thumbnailUrl }: IQuizItem) => {
    return (
        <div key={key} className="p-2 border border-blue-700 rounded-lg flex flex-col space-y-3">
            <img src={thumbnailUrl} alt={title} className="h-1/2 w-full rounded-sm" />
            <p className="text-lg font-bold flow-root">
                <span className="float-left">{title}</span>
                <span className="float-right text-sm text-gray-500">
                    {FormatHelper.durationConversionFromSeconds(duration)}
                </span>
            </p>
            <p className="text-sm">{description}</p>
        </div>
    );
}

export default QuizCard;