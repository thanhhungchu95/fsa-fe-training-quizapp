import { IconProp } from "@fortawesome/fontawesome-svg-core";

export default interface IBaseIconProps {
    icon: IconProp;
    text?: string;
    iconClassName?: string;
    className?: string;
}