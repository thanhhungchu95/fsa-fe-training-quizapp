import { ErrorMessage, Field, Form, Formik } from "formik";
import IContactModel from "../models/IContactModel";
import * as Yup from 'yup';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faLocationPin, faMailBulk, faPhone } from "@fortawesome/free-solid-svg-icons";

const Contact = () => {
    const initialValues: IContactModel = {
        name: '',
        email: '',
        message: '',
    };

    const validationSchema = Yup.object({
        name: Yup.string().required('Name must be provided')
            .min(5, 'Name must be at least 2 characters')
            .max(100, 'Name must be at most 50 characters'),
        email: Yup.string().required('Email must be provided')
            .email('Email is invalid')
            .min(6, 'Email must be at least 6 characters')
            .max(50, 'Email must be at most 50 characters'),
        message: Yup.string().required('Message must be provided')
            .min(10, 'Message must be at least 10 characters')
            .max(500, 'Message must be at most 500 characters'),
    });

    const onSubmit = (values: IContactModel) => {
        try {
            console.log('Contact form is submitted with these values: ' + JSON.stringify(values));
        } catch (error) {
            console.log('Error: ', error);
        }
    }

    const contactForm = (
        <Formik initialValues={initialValues} validationSchema={validationSchema} onSubmit={onSubmit}>
            <Form className="*:mb-3">
                <div className="form-group">
                    <label htmlFor="name" className='block mb-2'>Name</label>
                    <Field type="text" name="name" className="form-control p-2 border border-slate-300 rounded-sm w-full" 
                        placeholder="Enter your name" />
                    <ErrorMessage name="name" component="div" className="text-red-500" />
                </div>
                <div className="form-group">
                    <label htmlFor="email" className='block mb-2'>Email</label>
                    <Field type="text" name="email" className="form-control p-2 border border-slate-300 rounded-sm w-full" 
                        placeholder="Enter your email" />
                    <ErrorMessage name="email" component="div" className="text-red-500" />
                </div>
                <div className="form-group">
                    <label htmlFor="message" className='block mb-2'>Message</label>
                    <textarea name="message" className="form-control p-2 border border-slate-300 rounded-sm w-full"
                        placeholder="Enter your message" />
                    <ErrorMessage name="message" component="div" className="text-red-500" />
                </div>
                <div className="form-group">
                    <button type="submit" className="p-2 px-4 border bg-blue-500 hover:bg-blue-700 text-white rounded-lg">Send</button>
                </div>
            </Form>
        </Formik>
    );

    return (
        <div className="px-64">
            <section className="w-full flex flex-col items-center pb-4">
                <p className="uppercase font-bold text-4xl pt-8">Contact</p>
            </section>
            <section className="w-full flex space-x-2">
                <div className="form-login w-1/2 p-5">
                    <p className='text-xl font-bold'>Feedback</p>
                    <p className='text-sm mb-4'>Please fill out the form below to send us your feedback. We will get back to you as soon as possible.</p>
                    {contactForm}
                </div>
                <div className="w-1/2 p-5">
                    <p className='text-xl font-bold'>Our information</p>
                    <p className="text-sm mb-4">We are always here to help you. You can contact us through the following ways.</p>
                    <p className="text-md">
                        <FontAwesomeIcon icon={faMailBulk} /><span className="ml-2">thanhhungchu95@gmail.com</span> 
                    </p>
                    <p className="text-md">
                        <FontAwesomeIcon icon={faPhone} /><span className="ml-2 ">+84 326-917-817</span>
                    </p>
                    <p className="text-md">
                        <FontAwesomeIcon icon={faLocationPin} /><span className="ml-2">FPT Tower, 10 Pham Van Bach, Dich Vong, Cau Giay, Ha Noi</span>
                    </p>
                    <p className="text-md">
                    </p>
                </div>
            </section>
        </div>
    );
}

export default Contact;