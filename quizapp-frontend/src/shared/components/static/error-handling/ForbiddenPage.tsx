import BaseErrorPage from "./BaseErrorPage";

const ForbiddenPage = () => {
    return (
        <BaseErrorPage title="Forbidden" message="You don't have permission to access this page." statusCode={403} />
    );
};

export default ForbiddenPage;