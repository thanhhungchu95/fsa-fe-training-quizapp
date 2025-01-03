import { AxiosInstance } from "axios";
import { BaseApiService } from "./base.service";
import ISearchUserQuery from "../../models/queries/ISearchUserQuery";
import IUserDetailsModel from "../../models/users/IUserDetailsModel";

const api: AxiosInstance = BaseApiService.createApiServiceInstance('users');

const getAll = async () => {
    try {
        const response = await api.get('/');
        return response;
    } catch (error) {
        BaseApiService.handleError(error);
    }
};

const getById = async (id: string) => {
    try {
        const response: any = await api.get(`/${id}`);
        return response;
    } catch (error) {
        BaseApiService.handleError(error);
    }
};

const search = async (filter: ISearchUserQuery) => {
    try {
        const response: any = await api.get('/search', { params: filter });
        return response;
    } catch (error) {
        BaseApiService.handleError(error);
    }
}

const create = async (data: IUserDetailsModel) => {
    try {
        const response: any = await api.post('/', data);
        return response;
    } catch (error) {
        BaseApiService.handleError(error);
    }
}

const update = async (id: string, data: IUserDetailsModel) => {
    try {
        const response: any = await api.put(`/${id}`, data);
        return response;
    } catch (error) {
        BaseApiService.handleError(error);
    }
}

const remove = async (id: string) => {
    try {
        const response: any = await api.delete<boolean>(`/${id}`);
        return response;
    } catch (error) {
        BaseApiService.handleError(error);
    }
}

export const UserService = {
    getAll,
    getById,
    search,
    create,
    update,
    remove,
};