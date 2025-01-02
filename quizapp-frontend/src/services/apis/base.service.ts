import axios, { AxiosInstance } from "axios";

const createApiServiceInstance = (controller: string): AxiosInstance => {
    const api: AxiosInstance = axios.create({
        baseURL: process.env.REACT_APP_BASE_API_URL + controller,
        headers: {
            'Content-Type': 'application/json',
        },
    });
    
    api.interceptors.request.use((config) => {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }
        return config;
    });

    api.interceptors.response.use(
        (response) => response.data,
        (error) => {
            if (error.response) {
                const { status, data } = error.response;
                throw new Error(`API request failed with status ${status}: ${JSON.stringify(data)}`);
            } else if (error.request) {
                throw new Error('No response from the server');
            } else {
                throw new Error('Network error');
            }
        }
    );

    return api;
}

const handleError = (error: unknown): void => {
    let errorMessage = '';
    if (error instanceof Error) {
        errorMessage = error.message;
    } else if (typeof error === 'string') {
        errorMessage = error.toUpperCase();
    } else {
        errorMessage = 'An unexpected error occurred.';
    }
    throw new Error(`Error: ${errorMessage}`);
}

export const BaseApiService = {
    createApiServiceInstance,
    handleError,
};