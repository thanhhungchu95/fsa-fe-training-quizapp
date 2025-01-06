import * as Yup from 'yup';
import ILoginModel from '../../models/auths/ILoginModel';
import { ErrorMessage, Field, Form, Formik } from 'formik';
import { Link, useNavigate } from 'react-router-dom';
import { BaseApiService } from '../../services/apis/base.service';
import { useDispatch } from 'react-redux';
import { AppDispatch } from '../../stores/store';
import { login } from '../../features/auth/auth.thunk';

const Login = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch<AppDispatch>();

    const initialValues: ILoginModel = {
        username: '',
        password: '',
    };

    const validationSchema = Yup.object({
        username: Yup.string().required('Username must be provided'),
        password: Yup.string().required('Password must be provided')
            .min(6, 'Password must be at least 6 characters')
            .max(20, 'Password must be at most 20 characters'),
    });

    const onSubmit = async (values: ILoginModel) => {
        try {
            await dispatch(login(values));
            navigate('/');
        } catch (error) {
            BaseApiService.handleError(error);
        }
    }

    const loginForm = (
        <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit} >
            <Form className='*:mb-3'>
                <div className="form-group">
                    <label htmlFor="username" className='block mb-2'>Username</label>
                    <Field type="text" name="username" className="form-control p-2 border border-slate-300 rounded-sm w-full"
                        placeholder="Enter your username" />
                    <ErrorMessage name="username" component="div" className="text-red-500" />
                </div>
                <div className="form-group">
                    <label htmlFor="password" className='block mb-2'>Password</label>
                    <Field type="password" name="password" className="form-control p-2 border border-slate-300 rounded-sm w-full"
                        placeholder="Enter your password" />
                    <ErrorMessage name="password" component="div" className="text-red-500" />
                </div>
                <div className="form-group flex flex-row space-x-5">
                    <button className='p-2 px-4 border-2 hover:border-2 border-blue-500 bg-white hover:bg-slate-100 text-blue-500 rounded-full w-1/2' onClick={() => navigate('/')}>Back to Home</button>
                    <button type="submit" className="p-2 px-4 border bg-blue-500 hover:bg-blue-700 text-white rounded-full w-1/2">Login</button>
                </div>
                <div className="form-group text-center">
                    <label className="mr-3 text-center">Don't have an account?</label>
                    <Link to="/auth/register" className="text-blue-500 hover:text-blue-700">Register</Link>
                </div>
            </Form>
        </Formik>
    );

    return (
        <section className='h-screen w-full flex justify-center items-center'>
            <div className="form-login w-[450px] border border-slate-300 rounded-md shadow-md p-5 bg-white">
                <h1 className='text-2xl font-bold text-center mb-4'>Login</h1>
                {loginForm}
            </div>
        </section>
    );
}

export default Login;