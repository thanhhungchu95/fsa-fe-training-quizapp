import { faPerson, faQuestion, faUser, faUserCheck } from "@fortawesome/free-solid-svg-icons";
import IconLink from "./wrappers/IconLink";
import { useLocation } from "react-router-dom";

const SideBar = () => {
    const location = useLocation();

    const isActive = (path: string) => {
        return location.pathname === path;
    };

    const getLinkClass = (path: string) => {
        return `block p-3 ${
            isActive(path)
                ? "bg-blue-500 text-white"
                : "text-blue-500 hover:bg-blue-500 hover:text-white"
        }`;
    };
    return (
        <aside className="w-1/6 border-r-2 border-r-blue-200 h-full">
            <nav className="">
                <div className="sidebar-title text-2xl font-semibold p-3">Management</div>
                <ul>
                    <li>
                        <IconLink 
                            url="/management/quiz" 
                            icon={faPerson} 
                            text="Quiz Management" 
                            className={getLinkClass("/management/quiz")} 
                        />
                    </li>
                    <li>
                        <IconLink 
                            url="/management/question" 
                            icon={faQuestion} 
                            text="Question Management" 
                            className={getLinkClass("/management/question")} 
                        />
                    </li>
                    <li>
                        <IconLink 
                            url="/management/user" 
                            icon={faUser} 
                            text="User Management" 
                            className={getLinkClass("/management/user")} 
                        />
                    </li>
                    <li>
                        <IconLink 
                            url="/management/role" 
                            icon={faUserCheck} 
                            text="Role Management" 
                            className={getLinkClass("/management/role")} 
                        />
                    </li>
                </ul>
            </nav>
        </aside>
    );
}

export default SideBar;