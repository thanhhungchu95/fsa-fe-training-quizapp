import { faQuestionCircle } from "@fortawesome/free-solid-svg-icons";
import IconButton from "../wrappers/IconButton";
import quizbg from "../../../assets/pictures/quiz-bg-01.png";
import { toast } from "react-toastify";

const LandingContent = (props: { heading: string }) => {
    const takeAQuiz = (): void => {
        // Logic to take a quiz
        toast.info('Taking a quiz...');
    }

    return (
        <section className="w-full h-1/2 px-3.5 flex items-center">
            <div className="w-1/2 flex flex-col px-4 space-y-1 items-start">
                <p className="text-4xl font-bold">{props.heading}</p>
                <p className="text-lg">Discover new quizzes, share your knowledge, and expand your horizons.</p>
                <IconButton className="border border-blue-500 bg-blue-500 p-2 rounded-md text-white hover:text-blue-500 hover:bg-white hover:border hover:border-blue-500"
                    onClick={takeAQuiz} icon={faQuestionCircle} text={"Take a Quiz"} />
            </div>
            <div className="py-12 flex items-end flex-col">
                <img src={quizbg} alt={"Quiz Background"} className="w-1/2 h-1/2 pr-16" />
            </div>
        </section>
    );
}

export default LandingContent;