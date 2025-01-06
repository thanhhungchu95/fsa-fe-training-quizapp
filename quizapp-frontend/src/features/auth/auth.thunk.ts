import { createAsyncThunk } from "@reduxjs/toolkit";
import { UserViewModel } from "../../view-models/user/UserViewModel";
import ILoginModel from "../../models/auths/ILoginModel";
import { BaseApiService } from "../../services/apis/base.service";
import { AuthService } from "../../services/apis/auth.service";
import IRegisterModel from "../../models/auths/IRegisterModel";

export const login = createAsyncThunk<UserViewModel, ILoginModel>(
    'auth/login',
    async (loginRequest: ILoginModel, { rejectWithValue }) => {
        try {
            const response = await AuthService.login(loginRequest);
            return response;
        } catch (error: any) {
            BaseApiService.handleError(error);
            return rejectWithValue(error.response.message);
        }
    }
);

export const register = createAsyncThunk<UserViewModel, IRegisterModel>(
    'auth/register',
    async (registerRequest: IRegisterModel, { rejectWithValue }) => {
        try {
            const response = await AuthService.register(registerRequest);
            return response;
        } catch (error: any) {
            BaseApiService.handleError(error);
            return rejectWithValue(error.response.message);
        }
    }
);