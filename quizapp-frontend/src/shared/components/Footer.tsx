import { FormatHelper } from "../../helpers/FormatHelper";

const Footer = () => {
    return (
        <div className="flex justify-center items-center flex-col">
            &copy; Copy rights to this project and contributors under the MIT license
            <span>{FormatHelper.currentDateConversion()}</span>
        </div>
    );
};

export default Footer;