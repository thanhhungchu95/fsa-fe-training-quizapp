import React, { ReactNode } from "react";
import { Link } from "react-router-dom";

interface BaseErrorPageProps {
    title: string;
    message: string;
    statusCode?: number;
    children?: ReactNode;
    backToHomeLink?: string;
}

const BaseErrorPage: React.FC<BaseErrorPageProps> = ({
    title,
    message,
    statusCode,
    children,
    backToHomeLink = "/"
}) => {
    return (
        <div className="min-h-screen flex items-center justify-center bg-gradient-to-r from-indigo-500 from-10% via-sky-500 via-50% to-emerald-500 to-100%">
            <div className="bg-white p-16 rounded-lg shadow-lg text-center">
                {statusCode && (
                    <h1 className="text-7xl font-bold text-red-500 mb-4">{statusCode}</h1>
                )}
                <h2 className="text-3xl font-semibold mb-4">{title}</h2>
                <p className="text-lg text-gray-600 mb-6">{message}</p>
                {children}
                <Link 
                    to={backToHomeLink}
                    className="inline-block bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 transition-colors"
                >
                    Back to Home
                </Link>
            </div>
        </div>
    );
};

export default BaseErrorPage;