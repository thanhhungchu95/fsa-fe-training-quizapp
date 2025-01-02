import { IconProp } from "@fortawesome/fontawesome-svg-core";

interface IHeaderLinkProps {
    url: string;
    icon: IconProp;
    text: string;
    isShow?: boolean;
    className?: string;
}

export default IHeaderLinkProps;