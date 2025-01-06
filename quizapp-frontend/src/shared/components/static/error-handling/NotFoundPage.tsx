import BaseErrorPage from "./BaseErrorPage";

const NotFoundPage = () => {
    return (
        <BaseErrorPage title="Page Not Found" message="Sorry, the page you are looking for doesn't exist." statusCode={404} />
    );
};

export default NotFoundPage;