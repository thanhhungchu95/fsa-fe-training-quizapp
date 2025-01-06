import { Link } from "react-router-dom";
import IIconLinkProps from "../props/IIconLinkProps";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const IconLink: React.FC<IIconLinkProps> = ({ url, icon, text, className, onClick = () => {} }: IIconLinkProps) => {
    return (
        <Link to={url} className={className} onClick={onClick}>
            <FontAwesomeIcon icon={icon} /><span className="ml-1">{text}</span>
        </Link>
    );
}

export default IconLink;