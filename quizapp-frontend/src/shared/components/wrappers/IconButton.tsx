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
    iconPosition = "left",
}: IIconButtonProps) => {
    return (
        <button type={type} className={className} onClick={onClick} disabled={disabled} title={title}>
            {iconPosition === "left" && <FontAwesomeIcon icon={icon} className={`${iconClassName} mr-2`} />}
            {text.length ? <span>{text}</span> : ''}
            {iconPosition === "right" && <FontAwesomeIcon icon={icon} className={`${iconClassName} ml-2`} />}
        </button>
    );
}

export default IconButton;