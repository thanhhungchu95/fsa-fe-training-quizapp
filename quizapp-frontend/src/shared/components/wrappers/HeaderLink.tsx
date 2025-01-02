import IHeaderLinkProps from "../props/IHeaderLinkProps";
import IconLink from "./IconLink";

const HeaderLink: React.FC<IHeaderLinkProps> = ({ 
    url, 
    icon, 
    text, 
    isShow = true,
    className = ''
}: IHeaderLinkProps) => {
    return (
        <IconLink 
            url={url} 
            icon={icon} 
            text={text}
            className={`${className} ${isShow ? 'block' : 'hidden'}`} 
        />
    );
}

export default HeaderLink;