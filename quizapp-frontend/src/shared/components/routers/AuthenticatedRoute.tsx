import { useSelector } from "react-redux";
import { Navigate, Outlet } from "react-router";
import IAuthenticatedRouteModel from "../../../models/routes/IAuthenticatedRouteModel";
import ForbiddenPage from "../static/error-handling/ForbiddenPage";

const AuthenticatedRoute: React.FC<IAuthenticatedRouteModel> = ({ roles }) => {
    const { isAuthenticated, user } = useSelector((state: any) => state.auth);
    const userInformation = user ? JSON.parse(user.userInformation) : null;
    if (!isAuthenticated || !userInformation || !userInformation.roles) return <Navigate to="/auth/login" />;

    if (!roles.some(item => userInformation.roles.includes(item))) {
        return <ForbiddenPage />
    }

    return isAuthenticated? <Outlet /> : <Navigate to="/auth/login" />;
};

export default AuthenticatedRoute;