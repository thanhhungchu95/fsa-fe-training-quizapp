import { AxiosInstance } from "axios";
import { BaseApiService } from "./base.service";
import ISearchQuestionQuery from "../../models/queries/ISearchQuestionQuery";
import IQuestionDetailsModel from "../../models/questions/IQuestionDetailsModel";

const api: AxiosInstance = BaseApiService.createApiServiceInstance('questions');

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

const search = async (filter: ISearchQuestionQuery) => {
    try {
        const response: any = await api.get('/search', { params: filter });
        return response;
    } catch (error) {
        BaseApiService.handleError(error);
    }
};

const getQuestionsByQuizId = async (id: string) => {
    try {
        const response: any = await api.get(`getQuestionsByQuizId/${id}`);
        return response;
    } catch (error) {
        BaseApiService.handleError(error);
    }
};

const create = async (data: IQuestionDetailsModel) => {
    try {
        const response: any = await api.post('/', data);
        return response;
    } catch (error) {
        BaseApiService.handleError(error);
    }
};

const update = async (id: string, data: IQuestionDetailsModel) => {
    try {
        const response: any = await api.put(`/${id}`, data);
        return response;
    } catch (error) {
        BaseApiService.handleError(error);
    }
};

const remove = async (id: string) => {
    try {
        const response: any = await api.delete<boolean>(`/${id}`);
        return response;
    } catch (error) {
        BaseApiService.handleError(error);
    }
};

export const QuestionService = {
    getAll,
    getById,
    search,
    getQuestionsByQuizId,
    create,
    update,
    remove,
}