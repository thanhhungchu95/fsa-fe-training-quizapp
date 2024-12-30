import IHeaderLinkProps from "../props/IHeaderLinkProps";
import IconLink from "./IconLink";

const HeaderLink: React.FC<IHeaderLinkProps> = ({ url, icon, text, isShow }: IHeaderLinkProps) => {
    return (
        <IconLink url={url} icon={icon} text={text}
            className={`nav-link p-4 text-blue-500 hover:bg-blue-500 hover:text-white ${isShow ? 'block' : 'hidden'}`} />
    );
}

export default HeaderLink;