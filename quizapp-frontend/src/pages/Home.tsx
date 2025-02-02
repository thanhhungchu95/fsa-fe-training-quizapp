import IQuizItem from "../models/IQuizItem";
import thumbnail from "../assets/pictures/thumbnail.jpeg";
import { useEffect, useState } from "react";
import QuizCard from "../shared/components/QuizCard";
import LandingContent from "../shared/components/static/LandingContent";

const Home = () => {
    const [data, setData] = useState<IQuizItem[]>([]);

    useEffect(() => {
        setData(Array.from({ length: 3 }, (_, i) => i++).map((i) => {
            return {
                key: i,
                title: 'Capitals of Country',
                description: 'Test your knowledge of country capitals',
                duration: 900,
                thumbnailUrl: thumbnail,
            }
        }));
    }, []);

    return (
        <div className="px-64">
            <LandingContent heading={"Welcome to Quiz App"} />
            <section className="w-full px-3.5 flex flex-col items-center">
                <p className="uppercase font-bold text-4xl pb-4">Quizzes</p>
                <div className="grid grid-cols-3 gap-4 space-x-3 justify-items-stretch">
                    {
                        data.map((quiz: IQuizItem) => (
                            <QuizCard title={quiz.title} description={quiz.description} duration={quiz.duration} thumbnailUrl={quiz.thumbnailUrl} />
                        ))
                    }
                </div>
            </section>
        </div>
    );
}

export default Home;