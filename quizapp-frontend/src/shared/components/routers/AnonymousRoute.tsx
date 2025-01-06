import { useSelector } from "react-redux"
import { Navigate, Outlet } from "react-router-dom";

const AnonymousRoute: React.FC = () => {
    const { isAuthenticated } = useSelector((state: any) => state.auth);
    return isAuthenticated ? <Navigate to="/" /> : <Outlet />
}

export default AnonymousRoute;