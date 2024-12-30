import IQuizItem from "../models/IQuizItem";
import thumbnail from "../assets/pictures/thumbnail.jpeg";
import { useEffect, useState } from "react";
import QuizCard from "../shared/components/QuizCard";
import IQuizModel from "../models/IQuizModel";
import * as Yup from 'yup';
import { ErrorMessage, Field, Form, Formik } from "formik";

const Quizzes = () => {
    const [data, setData] = useState<IQuizItem[]>([]);

    const initialValues: IQuizModel = {
        code: '',
    };

    const validationSchema = Yup.object({
        code: Yup.string().required('Code is required'),
    });

    const onSubmit = (values: IQuizModel) => {
        try {
            console.log('Quiz is submitted with these values: ', JSON.stringify(values));
        } catch (error) {
            console.log('Error: ', error);
        }
    }

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

    const quizForm = (
        <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit} >
            <Form className="*:mb-3">
                <div className="flex flex-row">
                    <Field type="text" name="code" className="form-control p-2 border border-blue-500 outline-none rounded-tl-lg rounded-bl-lg w-full"
                        placeholder="Enter quiz code to take a quiz" />
                    <button type="submit" className="p-2 px-4 w-1/6 border border-blue-500 bg-blue-500 hover:bg-blue-700 text-white rounded-tr-lg rounded-br-lg">Take Quiz</button>
                </div>
                <ErrorMessage name="code" component="div" className="text-red-500" />
            </Form>
        </Formik>
    );

    return (
        <div className="px-64">
            <section className='w-full flex flex-col mb-32'>
                <p className="font-bold text-3xl py-4 text-center">Take a Quiz</p>
                {quizForm}
            </section>
            <section className="w-full px-3.5 flex flex-col items-center">
                <p className="uppercase font-bold text-4xl pb-4">Quizzes</p>
                <div className="grid grid-cols-3 gap-4 space-x-3 justify-items-stretch">
                    {
                        data.map((quiz: IQuizItem) => (
                            <QuizCard key={quiz.key} title={quiz.title} description={quiz.description} duration={quiz.duration} thumbnailUrl={quiz.thumbnailUrl} />
                        ))
                    }
                </div>
            </section>
        </div>
    );
}

export default Quizzes;