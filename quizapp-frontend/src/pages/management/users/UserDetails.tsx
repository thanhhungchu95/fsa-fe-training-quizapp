import { ErrorMessage, Field, Form, Formik } from "formik";
import IUserDetailsModel from "../../../models/users/IUserDetailsModel";
import { BaseApiService } from "../../../services/apis/base.service";
import { UserService } from "../../../services/apis/user.service";
import { UserViewModel } from "../../../view-models/user/UserViewModel";
import * as Yup from 'yup';
import Checkbox from "../../../core/components/Checkbox";
import IconButton from "../../../shared/components/wrappers/IconButton";
import { faCancel, faRefresh, faSave } from "@fortawesome/free-solid-svg-icons";

const UserDetails = ({ item, onCancel }: { item: UserViewModel, onCancel: any }) => {
    const initialValues: IUserDetailsModel = {
        firstName: item ? item.firstName : '',
        lastName: item ? item.lastName : '',
        email: item ? item.email : '',
        phoneNumber: item ? item.phoneNumber : '',
        isActive: item ? item.isActive : true,
        userName: item ? item.userName : '',
        avatar: item ? item.avatar : '',
        //dateOfBirth: (item ? moment(item.dateOfBirth) : moment()).toDate(),
    }

    if (!item) {
        Object.assign(initialValues, {
            password: '',
            confirmPassword: '',
        });
    }

    const validationSchema = Yup.object({
        firstName: Yup.string().required('First name must be provided')
            .min(2, 'First name must be at least 2 characters')
            .max(50, 'First name must be at most 50 characters'),
        lastName: Yup.string().required('Last name must be provided')
            .min(2, 'Last name must be at least 2 characters')
            .max(50, 'Last name must be at most 50 characters'),
        email: Yup.string().required('Email must be provided')
            .email('Email is invalid')
            .min(6, 'Email must be at least 6 characters')
            .max(50, 'Email must be at most 50 characters'),
        userName: Yup.string().required('Username must be provided'),
        phoneNumber: Yup.string().required('Phone number must be provided'),
        isActive: Yup.boolean().required("Active must be provided").default(true),
    });

    if (!item) {
        Object.assign(validationSchema, {
            password: Yup.string().required('Password must be provided')
                .min(8, 'Password must be at least 8 characters')
                .max(50, 'Password must be at most 50 characters'),
            confirmPassword: Yup.string().required('Confirm password must be provided')
                .oneOf([Yup.ref('password')], 'Passwords must match'),
        });
    }

    const onSubmit = async (values: IUserDetailsModel) => {
        console.log(values);
        try {
            let payload = {
                ...values,
                dateOfBirth: '1990-01-01', // Temporary
            }

            let response: any;
            if (item) {
                response = await UserService.update(item.id, payload);
            } else {
                response = await UserService.create(payload);
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

    const userForm = (
        <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit}>
            {({
                resetForm,
                setFieldValue,
                handleChange,
                handleBlur,
                touched,
                values,
                errors,
            }) => {
                return (
                    <Form className="card-body p-3 border-y border-slate-300 flex flex-wrap">
                        <div className="form-group w-1/2 p-2">
                            <label htmlFor="firstName" className='block mb-3'>First Name</label>
                            <Field type="text" name="firstName" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Enter user's first name" />
                            <ErrorMessage name="firstName" component="div" className="text-red-500" />
                        </div>
                        <div className="form-group w-1/2 p-2">
                            <label htmlFor="lastName" className='block mb-3'>Last Name</label>
                            <Field type="text" name="lastName" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Enter user's last name" />
                            <ErrorMessage name="lastName" component="div" className="text-red-500" />
                        </div>
                        <div className="form-group w-1/2 p-2">  
                            <label htmlFor="email" className='block mb-3'>Email</label>
                            <Field type="text" name="email" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Enter user's email" />
                            <ErrorMessage name="email" component="div" className="text-red-500" />
                        </div>
                        <div className="form-group w-1/2 p-2">
                            <label htmlFor="userName" className='block mb-3'>User Name</label>
                            <Field type="text" name="userName" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Enter user's username" />
                            <ErrorMessage name="userName" component="div" className="text-red-500" />
                        </div>
                        {!item && (
                        <div className="form-group w-1/2 p-2">
                            <label htmlFor="password" className='block mb-3'>Password</label>
                            <Field type="password" name="password" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Enter user's password" />
                            <ErrorMessage name="password" component="div" className="text-red-500" />
                        </div>)}
                        {!item && (<div className="form-group w-1/2 p-2">
                            <label htmlFor="confirmPassword" className='block mb-3'>Confirm Password</label>
                            <Field type="password" name="confirmPassword" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Confirm user's password" />
                            <ErrorMessage name="confirmPassword" component="div" className="text-red-500" />
                        </div>)}
                        <div className="form-group w-1/2 p-2">
                            <label htmlFor="phoneNumber" className='block mb-3'>Phone Number</label>
                            <Field type="text" name="phoneNumber" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Enter user's phone number" />
                            <ErrorMessage name="phoneNumber" component="div" className="text-red-500" />
                        </div>
                        <div className="form-group w-1/2 p-2">
                            <label htmlFor="avatar" className='block mb-3'>Avatar</label>
                            <Field type="text" name="avatar" className="p-2 border border-slate-300 rounded-sm w-full"
                                placeholder="Enter user's avatar url" />
                            <ErrorMessage name="avatar" component="div" className="text-red-500" />
                        </div>
                        {/* <div className="form-group w-1/2 p-2">
                            <label htmlFor="dateOfBirth" className='block mb-3'>Birthday</label>
                            <Field component={CustomDatePicker} name="dateOfBirth" className="" />
                            <ErrorMessage name="dateOfBirth" component="div" className="text-red-500" />
                        </div> */}
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
                    <h2 className="text-xl font-semibold">{item ? "Edit" : "Create"} User</h2>
                </div>
                {userForm}
            </div>
        </div>
    );
}

export default UserDetails;