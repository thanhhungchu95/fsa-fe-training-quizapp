import { faContactBook, faDashboard, faHome, faInfoCircle, faQuestionCircle, faSignIn, faSigning, faSignOut } from "@fortawesome/free-solid-svg-icons";
import HeaderLink from "./wrappers/HeaderLink";
import { Link } from "react-router-dom";
import logo from "../../assets/pictures/quiz-logo.svg";
import { useState } from "react";

const Header = () => {
    // Temporary variable
    const [isAuthenticated] = useState<boolean>(false);
    const [isManager] = useState<boolean>(false);
    const [userName] = useState<string>('');

    return (
        <header className="flex justify-between items-center px-4 border-b-2 border-blue-500 text-blue-700">
            <Link to="/" className="brand text-2xl font-bold flex items-center">
                <img src={logo} alt={"Quizzes"} className="h-12" />
            </Link>
            <nav>
                <ul className="nav-menu flex justify-center">
                    <li className="nav-item">
                        <HeaderLink url={"/"} text={"Home"} icon={faHome} isShow={true} />
                    </li>
                    <li className="nav-item">
                        <HeaderLink url={"/quizzes"} text={"Quizzes"} icon={faQuestionCircle} isShow={true} />
                    </li>
                    <li className="nav-item">
                        <HeaderLink url={"/management/dashboard"} text={"Management"} icon={faDashboard} isShow={isAuthenticated && isManager} />
                    </li>
                    <li className="nav-item">
                        <HeaderLink url={"/about"} text={"About"} icon={faInfoCircle} isShow={true} />
                    </li>
                    <li className="nav-item">
                        <HeaderLink url={"/contact"} text={"Contact"} icon={faContactBook} isShow={true} />
                    </li>
                </ul>
            </nav>
            <div className="profile-menu">
                <ul className="nav-menu flex justify-center">
                    <li className="nav-item">
                        <HeaderLink url={"/auth/login"} text={"Login"} icon={faSignIn} isShow={!isAuthenticated} />
                    </li>
                    <li className="nav-item">
                        <HeaderLink url={"/auth/register"} text={"Register"} icon={faSigning} isShow={!isAuthenticated} />
                    </li>
                    <li className="nav-item items-center p-4 flex">
                        <span className={isAuthenticated ? "" : "hidden"}>Welcome, {userName}</span>
                    </li>
                    <li className="nav-item">
                        <HeaderLink url={"/auth/logout"} text={"Logout"} icon={faSignOut} isShow={isAuthenticated} />
                    </li>
                </ul>
            </div>
        </header>
    );
};

export default Header;