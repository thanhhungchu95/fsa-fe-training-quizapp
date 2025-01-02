import { IconProp } from "@fortawesome/fontawesome-svg-core";
import IBaseIconProps from "./IBaseIconProps";

export default interface IIconButtonProps extends IBaseIconProps {
    onClick?: () => void;
    disabled?: boolean;
    title?: string;
    type?: "button" | "reset" | "submit" | undefined;
}