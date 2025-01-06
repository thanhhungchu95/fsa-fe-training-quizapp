import { AxiosInstance } from "axios";
import { BaseApiService } from "./base.service";
import ILoginModel from "../../models/auths/ILoginModel";
import { toast } from "react-toastify";
import IRegisterModel from "../../models/auths/IRegisterModel";

const api: AxiosInstance = BaseApiService.createApiServiceInstance('auth');

const login = async (data: ILoginModel) => {
    const response: any = await api.post('/login', data);
    if (response) {
        toast.success('Login successful');
    } else {
        toast.warning('No data returned from server. Please try again');
    }

    return response;
};

const register = async (data: IRegisterModel) => {
    const response: any = await api.post('/register', data);
    if (response) {
        toast.success('Registration successful');
    } else {
        toast.warning('No data returned from server. Please try again');
    }

    return response;
};

export const AuthService = {
    login,
    register,
};