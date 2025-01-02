import { ErrorMessage, Field, Form, Formik } from "formik";
import IQuizDetailsModel from "../../../models/quizzes/IQuizDetailsModel";
import { BaseApiService } from "../../../services/apis/base.service";
import { QuizService } from "../../../services/apis/quiz.service";
import { QuizViewModel } from "../../../view-models/quiz/QuizViewModel";
import * as Yup from 'yup';
import Checkbox from "../../../core/components/Checkbox";

const QuizDetails = ({ item, onCancel }: { item: QuizViewModel, onCancel: any }) => {
    const initialValues: IQuizDetailsModel = {
        title: item ? item.title : '',
        description: item ? item.description : '',
        questionIdWithOrders: [],
        duration: item ? item.duration : 0,
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
                questionIdWithOrders: [],
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
            <Form className="card-body p-3 border-y border-slate-300 flex flex-wrap">
                <div className="form-group w-1/2 p-2">
                    <label htmlFor="title" className='block mb-3'>Title</label>
                    <Field type="text" name="title" className="p-2 border border-slate-300 rounded-sm w-full"
                        placeholder="Enter quiz's title" />
                    <ErrorMessage name="title" component="div" className="text-red-500" />
                </div>
                <div className="form-group w-1/2 p-2">
                    <label htmlFor="description" className='block mb-3'>Description</label>
                    <Field type="text" name="description" className="p-2 border border-slate-300 rounded-sm w-full"
                        placeholder="Enter quiz's description" />
                    <ErrorMessage name="description" component="div" className="text-red-500" />
                </div>
                <div className="form-group w-1/3 p-2">
                    <label htmlFor="duration" className='block mb-3'>Duration</label>
                    <Field type="number" name="duration" className="p-2 border border-slate-300 rounded-sm w-full"
                        placeholder="Enter quiz's duration" />
                    <ErrorMessage name="duration" component="div" className="text-red-500" />
                </div>
                <div className="form-group w-1/2 p-2">
                    <label htmlFor="thumbnailUrl" className='block mb-3'>Picture</label>
                    <Field type="text" name="thumbnailUrl" className="p-2 border border-slate-300 rounded-sm w-full"
                        placeholder="Enter quiz's description" />
                    <ErrorMessage name="thumbnailUrl" component="div" className="text-red-500" />
                </div>
                <div className="form-group w-1/6 p-2">
                    <label htmlFor="isActive" className='block mb-3'>Active?</label>
                    <Checkbox title={"isActive"} defaultChecked={item ? item.isActive : true} />
                    <ErrorMessage name="isActive" component="div" className="text-red-500" />
                </div>
            </Form>
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