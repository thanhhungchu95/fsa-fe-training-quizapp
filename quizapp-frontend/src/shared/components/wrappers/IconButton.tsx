import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import IIconButtonProps from "../props/IIconButtonProps";

const IconButton: React.FC<IIconButtonProps> = ({ icon, text, onClick, className }: IIconButtonProps) => {
    return (
        <button className={className} onClick={onClick}>
            <FontAwesomeIcon icon={icon} /><span className="ml-1">{text}</span>
        </button>
    );
}

export default IconButton;