import { faContactBook, faDashboard, faHome, faInfoCircle, faQuestionCircle, faSignIn, faSigning, faSignOut } from "@fortawesome/free-solid-svg-icons";
import HeaderLink from "./wrappers/HeaderLink";
import { Link, useLocation } from "react-router-dom";
import logo from "../../assets/pictures/quiz-logo.svg";
import { useState } from "react";

const Header = () => {
    // Temporary variable
    const [isAuthenticated] = useState<boolean>(true);
    const [isManager] = useState<boolean>(true);
    const [userName] = useState<string>('');

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
                    <li className="nav-item">
                        <HeaderLink url="/management/dashboard" text="Management" icon={faDashboard} isShow={isAuthenticated && isManager} className={getLinkClass("/management/dashboard")} />
                    </li>
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
                    <li className="nav-item">
                        <HeaderLink url="/auth/login" text="Login" icon={faSignIn} isShow={!isAuthenticated} className={getLinkClass("/auth/login")} />
                    </li>
                    <li className="nav-item">
                        <HeaderLink url="/auth/register" text="Register" icon={faSigning} isShow={!isAuthenticated} className={getLinkClass("/auth/register")} />
                    </li>
                    <li className="nav-item items-center p-4 flex">
                        <span className={isAuthenticated ? "" : "hidden"}>Welcome, {userName}</span>
                    </li>
                    <li className="nav-item">
                        <HeaderLink url="/auth/logout" text="Logout" icon={faSignOut} isShow={isAuthenticated} className={getLinkClass("/auth/logout")} />
                    </li>
                </ul>
            </div>
        </header>
    );
};

export default Header;