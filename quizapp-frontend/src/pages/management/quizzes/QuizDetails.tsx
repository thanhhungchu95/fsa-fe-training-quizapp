import { ErrorMessage, Field, Form, Formik } from "formik";
import IQuizDetailsModel from "../../../models/quizzes/IQuizDetailsModel";
import { BaseApiService } from "../../../services/apis/base.service";
import { QuizService } from "../../../services/apis/quiz.service";
import { QuizViewModel } from "../../../view-models/quiz/QuizViewModel";
import * as Yup from 'yup';
import Checkbox from "../../../core/components/Checkbox";
import IconButton from "../../../shared/components/wrappers/IconButton";
import { faCancel, faRefresh, faSave } from "@fortawesome/free-solid-svg-icons";
import { useEffect, useState } from "react";
import QuestionIdWithOrderModel from "../../../models/questions/QuestionIdWithOrderModel";
import { QuestionService } from "../../../services/apis/question.service";

const QuizDetails = ({ item, onCancel }: { item: QuizViewModel, onCancel: any }) => {
    const [questionIdWithOrders, setQuestionIdWithOrders] = useState<QuestionIdWithOrderModel[]>([]);

    useEffect(() => {
        const fetchQuestions = async (): Promise<void> => {
            try {
                if (item?.id) {
                    const questionsByQuiz = await QuestionService.getQuestionsByQuizId(item.id);
                    setQuestionIdWithOrders(questionsByQuiz.map((q: any) => {
                        return {
                            questionId: q.id,
                            order: q.order,
                        };
                    }));
                }
            } catch (error) {
                BaseApiService.handleError(error);
            }
        }

        fetchQuestions();
    }, [item]);

    const initialValues: IQuizDetailsModel = {
        title: item ? item.title : '',
        description: item ? item.description : '',
        questionIdWithOrders: questionIdWithOrders,
        duration: item ? item.duration : 1,
        isActive: item ? item.isActive : true,
        thumbnailUrl: item ? item.thumbnailUrl : '',
    }

    const validationSchema = Yup.object({
        title: Yup.string().required("Title must be provided")
            .min(3, "Title must be at least 3 characters")
            .max(50, "Title must be at most 50 characters"),
        description: Yup.string().required("Description must be provided")
            .max(500, "Description must be at most 500 characters"),
        duration: Yup.number().required("Duration must be provided")
            .min(1, "Duration must be greater than or equal to 1"),
        isActive: Yup.boolean().required("Active must be provided").default(true),
    });

    const onSubmit = async (values: IQuizDetailsModel) => {
        try {
            let params = {
                ...values,
                questionIdWithOrders,
            };

            let response: any;
            if (item) {
                response = await QuizService.update(item.id, params);
            } else {
                response = await QuizService.create(params);
            }

            if (response) {
                onCancel();
            } else {
                BaseApiService.handleError(`Error: ${response}`);
            }
        } catch (error) {
            BaseApiService.handleError(error);
        }
    }

    const quizForm = (
        <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit}>
            {({
                resetForm,
                handleChange,
                handleBlur,
                touched,
                values,
                errors,
            }) => {
                return (
                    <Form className="card-body p-3 border-y border-slate-300 flex flex-wrap">
                        <div className="form-group w-1/2 p-2">
                            <label htmlFor="title" className='block mb-3'>Title</label>
                            <Field type="text" name="title" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Enter quiz's title" />
                            <ErrorMessage name="title" component="div" className="text-red-500" />
                        </div>
                        <div className="form-group w-1/2 p-2">
                            <label htmlFor="description" className='block mb-3'>Description</label>
                            <textarea name="description" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Enter quiz's description"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                value={values.description} />
                            {
                                touched.description && errors.description ? (
                                    <div className="text-red-500">
                                        {typeof errors.description === 'string' ? errors.description : ''}
                                    </div>
                                ) : null
                            }
                        </div>
                        <div className="form-group w-1/3 p-2">
                            <label htmlFor="duration" className='block mb-3'>Duration</label>
                            <Field type="number" name="duration" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Enter quiz's duration" min={1} />
                            <ErrorMessage name="duration" component="div" className="text-red-500" />
                        </div>
                        <div className="form-group w-1/2 p-2">
                            <label htmlFor="thumbnailUrl" className='block mb-3'>Picture</label>
                            <Field type="text" name="thumbnailUrl" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Enter quiz's picture url" />
                            <ErrorMessage name="thumbnailUrl" component="div" className="text-red-500" />
                        </div>
                        <div className="form-group w-1/6 p-2">
                            <label htmlFor="isActive" className='block mb-3'>Active?</label>
                            <Checkbox title={"isActive"} defaultChecked={item ? item.isActive : true} />
                            <ErrorMessage name="isActive" component="div" className="text-red-500" />
                        </div>
                        <div className="form-group p-2 w-full">
                            <div className="card-footer p-3 flow-root">
                                <IconButton className={"p-2 px-4 bg-slate-200 hover:bg-slate-300 rounded-full float-left"} onClick={onCancel}
                                    icon={faCancel} iconClassName={"mr-2"} text={"Cancel"} />
                                <div className="space-x-3 float-right">
                                    <IconButton type={"reset"} className={"p-2 px-4 bg-slate-300 text-white hover:bg-slate-500 rounded-full"} onClick={resetForm}
                                        icon={faRefresh} iconClassName={"mr-2"} text={"Reset"} />
                                    <IconButton type={"submit"} className={"p-2 px-4 bg-blue-500 text-white hover:bg-blue-700 rounded-full"}
                                        icon={faSave} iconClassName={"mr-2"} text={"Save"} />
                                </div>
                            </div>
                        </div>
                    </Form>
                );
            }}
            
        </Formik>
    )

    return (
        <div className="w-full mb-auto">
            <div className="card border border-slate-300 rounded-md">
                <div className="card-header p-3">
                    <h2 className="text-xl font-semibold">{item ? "Edit" : "Create"} Quiz</h2>
                </div>
                {quizForm}
            </div>
        </div>
    );
}

export default QuizDetails;