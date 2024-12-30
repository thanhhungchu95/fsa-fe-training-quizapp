import { faPerson, faQuestion, faUser, faUserCheck } from "@fortawesome/free-solid-svg-icons";
import IconLink from "./wrappers/IconLink";

const SideBar = () => {
    return (
        <aside className="w-1/6 border-r-2  border-blue-500">
            <nav>
                <div className="sidebar-title text-2xl font-semibold p-3">Management</div>
                <ul>
                    <li>
                        <IconLink url={"/management/quiz"} icon={faPerson} text={"Quiz Management"} className={"block text-blue-500 p-3 hover:bg-blue-500 hover:text-white"} />
                    </li>
                    <li>
                        <IconLink url={"/management/question"} icon={faQuestion} text={"Question Management"} className={"block text-blue-500 p-3 hover:bg-blue-500 hover:text-white"} />
                    </li>
                    <li>
                        <IconLink url={"/management/user"} icon={faUser} text={"User Management"} className={"block text-blue-500 p-3 hover:bg-blue-500 hover:text-white"} />
                    </li>
                    <li>
                        <IconLink url={"/management/role"} icon={faUserCheck} text={"Role Management"} className={"block text-blue-500 p-3 hover:bg-blue-500 hover:text-white"} />
                    </li>
                </ul>
            </nav>
        </aside>
    );
}

export default SideBar;