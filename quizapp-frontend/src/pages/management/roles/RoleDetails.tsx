import { ErrorMessage, Field, Form, Formik } from "formik";
import IRoleDetailsModel from "../../../models/roles/IRoleDetailsModel";
import { BaseApiService } from "../../../services/apis/base.service";
import { RoleService } from "../../../services/apis/role.service";
import { RoleViewModel } from "../../../view-models/role/RoleViewModel";
import * as Yup from 'yup';
import Checkbox from "../../../core/components/Checkbox";
import IconButton from "../../../shared/components/wrappers/IconButton";
import { faCancel, faRefresh, faSave } from "@fortawesome/free-solid-svg-icons";

const RoleDetails = ({ item, onCancel }: { item: RoleViewModel, onCancel: any }) => {
    const initialValues: IRoleDetailsModel = {
        name: item ? item.name : '',
        description: item ? item.description : '',
        isActive: item ? item.isActive : true,
    }

    const validationSchema = Yup.object({
        name: Yup.string().required("Name must be provided")
            .min(3, "Name must be at least 3 characters")
            .max(50, "Name must be at most 50 characters"),
        description: Yup.string().required("Description must be provided")
            .max(500, "Description must be at most 500 characters"),
        isActive: Yup.boolean().required("Active must be provided").default(true),
    });

    const onSubmit = async (values: IRoleDetailsModel) => {
        try {
            let response: any;
            if (item) {
                response = await RoleService.update(item.id, values);
            } else {
                response = await RoleService.create(values);
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
                        <div className="form-group w-1/3 p-2">
                            <label htmlFor="name" className='block mb-3'>Name</label>
                            <Field type="text" name="name" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Enter role's name" />
                            <ErrorMessage name="name" component="div" className="text-red-500" />
                        </div>
                        <div className="form-group w-1/2 p-2">
                            <label htmlFor="description" className='block mb-3'>Description</label>
                            <textarea name="description" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Enter role's description"
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
                    <h2 className="text-xl font-semibold">{item ? "Edit" : "Create"} Role</h2>
                </div>
                {roleForm}
            </div>
        </div>
    );
}

export default RoleDetails;