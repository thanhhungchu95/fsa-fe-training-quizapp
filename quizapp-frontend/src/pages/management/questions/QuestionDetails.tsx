import { ErrorMessage, Field, Form, Formik } from "formik";
import IQuestionDetailsModel from "../../../models/questions/IQuestionDetailsModel";
import { BaseApiService } from "../../../services/apis/base.service";
import { QuestionViewModel } from "../../../view-models/question/QuestionViewModel";
import * as Yup from 'yup';
import Checkbox from "../../../core/components/Checkbox";
import IconButton from "../../../shared/components/wrappers/IconButton";
import { faCancel, faRefresh, faSave } from "@fortawesome/free-solid-svg-icons";
import QuestionTypeEnum from "../../../core/enums/QuestionTypeEnum";
import { QuestionService } from "../../../services/apis/question.service";
import { EnumHelper } from "../../../helpers/EnumHelper";

const RoleDetails = ({ item, onCancel }: { item: QuestionViewModel, onCancel: any }) => {
    const initialValues: IQuestionDetailsModel = {
        content: item ? item.content : '',
        questionType: item ? item.questionType : QuestionTypeEnum.MultipleChoice,
        isActive: item ? item.isActive : true,
    }

    const validationSchema = Yup.object({
        content: Yup.string().required("Content must be provided")
            .min(3, "Content must be at least 3 characters")
            .max(50, "Content must be at most 50 characters"),
        questionType: Yup.string().required("Question Type must be provided"),
        isActive: Yup.boolean().required("Active must be provided").default(true),
    });

    const onSubmit = async (values: IQuestionDetailsModel) => {
        try {
            let payload = {
                ...values,
                questionType: Number(values.questionType),
            }
            let response: any;
            if (item) {
                response = await QuestionService.update(item.id, payload);
            } else {
                response = await QuestionService.create(payload);
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

    const roleForm = (
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
                        <div className="form-group w-2/3 p-2">
                            <label htmlFor="content" className='block mb-3'>Content</label>
                            <Field type="text" name="content" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Enter question's content" />
                            <ErrorMessage name="content" component="div" className="text-red-500" />
                        </div>
                        <div className="form-group w-1/6 flex flex-col p-2">
                            <label htmlFor="questionType" className='block mb-3'>Question Type</label>
                            <select name="questionType" id="questionType" title="Select Question Type"
                                onChange={handleChange}
                                onBlur={handleBlur}
                                value={values.questionType}
                                className="p-2 border border-slate-300 rounded-sm">
                                <option value="">Select Question Type</option>
                                {
                                    EnumHelper.convertEnumToArray(QuestionTypeEnum).map((type) => (
                                        <option key={type.key} value={type.key}>{type.value}</option>
                                    ))
                                }
                            </select>
                            {
                                touched.questionType && errors.questionType ? (
                                    <div className="text-red-500">
                                        {typeof errors.questionType === 'string' ? errors.questionType : ''}
                                    </div>
                                ) : null
                            }
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
                    <h2 className="text-xl font-semibold">{item ? "Edit" : "Create"} Question</h2>
                </div>
                {roleForm}
            </div>
        </div>
    );
}

export default RoleDetails;