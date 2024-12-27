import IBaseIconProps from "./IBaseIconProps";

export default interface IIconButtonProps extends IBaseIconProps {
    onClick: () => void;
    className: string;
}