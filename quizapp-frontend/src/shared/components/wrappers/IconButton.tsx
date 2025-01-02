import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import IIconButtonProps from "../props/IIconButtonProps";

const IconButton: React.FC<IIconButtonProps> = ({
    disabled = false,
    onClick = () => {},
    text = '',
    className = '',
    iconClassName = '',
    icon,
    title = '',
    type = 'button',
}: IIconButtonProps) => {
    return (
        <button type={type} className={className} onClick={onClick} disabled={disabled} title={title}>
            <FontAwesomeIcon icon={icon} className={iconClassName} />{text.length ? <span className="ml-2">{text}</span> : ''}
        </button>
    );
}

export default IconButton;