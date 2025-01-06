import IHeaderLinkProps from "../props/IHeaderLinkProps";
import IconLink from "./IconLink";

const HeaderLink: React.FC<IHeaderLinkProps> = ({ 
    url, 
    icon, 
    text, 
    className = '',
    onClick = () => {}
}: IHeaderLinkProps) => {
    return (
        <IconLink 
            url={url} 
            icon={icon} 
            text={text}
            className={className}
            onClick={onClick}
        />
    );
}

export default HeaderLink;