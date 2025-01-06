import * as Yup from 'yup';
import IRegisterModel from '../../models/auths/IRegisterModel';
import { ErrorMessage, Field, Form, Formik } from 'formik';
import { Link, useNavigate } from 'react-router-dom';
import { RegularExpression } from '../../core/constants/RegularExpression';
import moment from 'moment';
import { AppDispatch } from '../../stores/store';
import { useDispatch } from 'react-redux';
import { register } from '../../features/auth/auth.thunk';
import { BaseApiService } from '../../services/apis/base.service';

const Register = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch<AppDispatch>();

    const initialValues: IRegisterModel = {
        username: '',
        email: '',
        password: '',
        confirmPassword: '',
        firstName: '',
        lastName: '',
        phoneNumber: '',
        // dateOfBirth: new Date(),
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
        username: Yup.string().required('Username must be provided'),
        password: Yup.string().required('Password must be provided')
            .min(6, 'Password must be at least 6 characters')
            .max(20, 'Password must be at most 20 characters'),
        confirmPassword: Yup.string().required('Confirm password must be provided')
            .oneOf([Yup.ref('password')], 'Passwords must match'),
        phoneNumber: Yup.string().required('Phone number must be provided')
            .matches(RegularExpression.phoneRegExp, 'Phone number is not valid'),
        // dateOfBirth: Yup.date().required('Date of birth must be provided')
        //     .max(moment().date(), `Date of birth cannot be exceeded ${moment().format('DD/MM/yyyy')}`),
    });

    const onSubmit = async (values: IRegisterModel) => {
        try {
            await dispatch(register({ ...values, dateOfBirth: new Date('1990-01-01') }));
            navigate('/');
        } catch (error) {
            BaseApiService.handleError(error);
        }
    }

    const registerForm = (
        <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit} >
            <Form className='flex flex-wrap *:mb-3 *:w-1/2 *:px-2'>
                <div className="form-group">
                    <label htmlFor="firstName" className='block mb-2'>First Name</label>
                    <Field type="text" name="firstName" className="form-control p-2 border border-slate-300 rounded-sm w-full" />
                    <ErrorMessage name="firstName" component="div" className="text-red-500" />
                </div>
                <div className="form-group">
                    <label htmlFor="lastName" className='block mb-2'>Last Name</label>
                    <Field type="text" name="lastName" className="form-control p-2 border border-slate-300 rounded-sm w-full" />
                    <ErrorMessage name="lastName" component="div" className="text-red-500" />
                </div>
                <div className="form-group">
                    <label htmlFor="email" className='block mb-2'>Email</label>
                    <Field type="email" name="email" className="form-control p-2 border border-slate-300 rounded-sm w-full" />
                    <ErrorMessage name="email" component="div" className="text-red-500" />
                </div>
                <div className="form-group">
                    <label htmlFor="username" className='block mb-2'>Username</label>
                    <Field type="text" name="username" className="form-control p-2 border border-slate-300 rounded-sm w-full" />
                    <ErrorMessage name="username" component="div" className="text-red-500" />
                </div>
                <div className="form-group">
                    <label htmlFor="password" className='block mb-2'>Password</label>
                    <Field type="password" name="password" className="form-control p-2 border border-slate-300 rounded-sm w-full" />
                    <ErrorMessage name="password" component="div" className="text-red-500" />
                </div>
                <div className="form-group">
                    <label htmlFor="confirmPassword" className='block mb-2'>Confirm Password</label>
                    <Field type="password" name="confirmPassword" className="form-control p-2 border border-slate-300 rounded-sm w-full" />
                    <ErrorMessage name="confirmPassword" component="div" className="text-red-500" />
                </div>
                <div className="form-group">
                    <label htmlFor="phoneNumber" className='block mb-2'>Phone Number</label>
                    <Field type="text" name="phoneNumber" className="form-control p-2 border border-slate-300 rounded-sm w-full" />
                    <ErrorMessage name="phoneNumber" component="div" className="text-red-500" />
                </div>
                {/* <div className="form-group">
                    <label htmlFor="dateOfBirth" className='block mb-2'>Date of Birth</label>
                    <Field type="date" name="dateOfBirth" className="form-control p-2 border border-slate-300 rounded-sm w-full" />
                    <ErrorMessage name="dateOfBirth" component="div" className="text-red-500" />
                </div> */}
                <div className="form-group !w-full">
                    <button className='p-2 px-4 border-2 hover:border-2 border-blue-500 bg-white hover:bg-slate-100 text-blue-500 rounded-full w-1/2' onClick={() => navigate('/')}>Back to Home</button>
                    <button type="submit" className="p-2 px-4 border bg-blue-500 hover:bg-blue-700 text-white rounded-full w-1/2">Register</button>
                </div>
                <div className="form-group text-center !w-full">
                    <label htmlFor="login" className="mr-3 text-center">Already have an account?</label>
                    <Link to="/auth/login" className="text-blue-500 hover:text-blue-700">Login</Link>
                </div>
            </Form>
        </Formik>
    );

    return (
        <section className='h-screen w-full flex justify-center items-center'>
            <div className="form-login w-[550px] border border-slate-300 rounded-md shadow-md p-5 bg-white">
                <h1 className='text-2xl font-bold text-center mb-4'>Register</h1>
                {registerForm}
            </div>
        </section>
    );
}

export default Register;