import { faAngleDown, faContactBook, faDashboard, faHome, faInfoCircle, faQuestionCircle, faSignIn, faSigning, faSignOut } from "@fortawesome/free-solid-svg-icons";
import HeaderLink from "./wrappers/HeaderLink";
import { Link, useLocation, useNavigate } from "react-router-dom";
import logo from "../../assets/pictures/quiz-logo.svg";
import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { AppDispatch } from "../../stores/store";
import IconButton from "./wrappers/IconButton";
import { logout } from "../../features/auth/auth.slice";
import { toast } from "react-toastify";

const Header = () => {
    const dispatch = useDispatch<AppDispatch>();
    const { isAuthenticated, user } = useSelector((state: any) => state.auth);
    const userInformation = user ? JSON.parse(user.userInformation) : null;
    const canManage = userInformation ? userInformation.roles.filter((r: string) => r === 'Admin' || r === 'Editor').length > 0 : false;
    const navigate = useNavigate();
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);

    const location = useLocation();

    const isActive = (path: string) => {
        return location.pathname === path;
    };

    const getLinkClass = (path: string) => {
        return `nav-link p-4 ${
            isActive(path)
                ? "bg-blue-500 text-white"
                : "text-blue-500 hover:bg-blue-500 hover:text-white"
        }`;
    };

    const onLogout = () => {
        dispatch(logout());
        toast.success('Logout successfully');
        navigate('/');
    }

    return (
        <header className="flex justify-between items-center px-4 border-b-2 border-b-blue-200 text-blue-500">
            <Link to="/" className="brand text-2xl font-bold flex items-center">
                <img src={logo} alt={"Quizzes"} className="h-12" />
            </Link>
            <nav>
                <ul className="nav-menu flex justify-center">
                    <li className="nav-item">
                        <HeaderLink url="/" text="Home" icon={faHome} className={getLinkClass("/")} />
                    </li>
                    <li className="nav-item">
                        <HeaderLink url="/quizzes" text="Quizzes" icon={faQuestionCircle} className={getLinkClass("/quizzes")} />
                    </li>
                    {canManage && <li className="nav-item">
                        <HeaderLink url="/management/dashboard" text="Management" icon={faDashboard} className={getLinkClass("/management/dashboard")} />
                    </li>}
                    <li className="nav-item">
                        <HeaderLink url="/about" text="About" icon={faInfoCircle} className={getLinkClass("/about")} />
                    </li>
                    <li className="nav-item">
                        <HeaderLink url="/contact" text="Contact" icon={faContactBook} className={getLinkClass("/contact")} />
                    </li>
                </ul>
            </nav>
            <div className="profile-menu">
                <ul className="nav-menu flex justify-center">
                    {!isAuthenticated &&
                        <li className="nav-item">
                            <HeaderLink url="/auth/login" text="Login" icon={faSignIn} className={getLinkClass("/auth/login")} />
                        </li>
                    }
                    {!isAuthenticated &&
                        <li className="nav-item">
                            <HeaderLink url="/auth/register" text="Register" icon={faSigning} className={getLinkClass("/auth/register")} />
                        </li>
                    }
                    
                    {isAuthenticated && (
                        <li className="nav-item relative">
                            <IconButton onClick={() => setIsDropdownOpen(!isDropdownOpen)} icon={faAngleDown} iconPosition="right"
                                className={"flex items-center p-4 text-blue-500 hover:bg-blue-500 hover:text-white focus:outline-none"}
                                text={`Welcome, ${userInformation ? userInformation.displayName : 'User'}`} />
                            {isDropdownOpen && (
                                <ul className="absolute right-0 mt-2 py-2 w-48 bg-white rounded-md shadow-xl z-20 border border-blue-700">
                                    <li>
                                        <IconButton onClick={() => { onLogout(); setIsDropdownOpen(false); }}
                                            className={"block px-4 py-2 text-md text-blue-500 hover:bg-blue-500 hover:text-white w-full text-left"}
                                            icon={faSignOut} text="Logout" />
                                    </li>
                                </ul>
                            )}
                        </li>
                    )}
                </ul>
            </div>
        </header>
    );
};

export default Header;