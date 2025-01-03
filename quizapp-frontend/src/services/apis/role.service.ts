import { AxiosInstance } from "axios";
import { BaseApiService } from "./base.service";
import ISearchRoleQuery from "../../models/queries/ISearchRoleQuery";
import IRoleDetailsModel from "../../models/roles/IRoleDetailsModel";

const api: AxiosInstance = BaseApiService.createApiServiceInstance('roles');

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

const search = async (filter: ISearchRoleQuery) => {
    try {
        const response: any = await api.get('/search', { params: filter });
        return response;
    } catch (error) {
        BaseApiService.handleError(error);
    }
}

const create = async (data: IRoleDetailsModel) => {
    try {
        const response: any = await api.post('/', data);
        return response;
    } catch (error) {
        BaseApiService.handleError(error);
    }
}

const update = async (id: string, data: IRoleDetailsModel) => {
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

export const RoleService = {
    getAll,
    getById,
    search,
    create,
    update,
    remove,
};
