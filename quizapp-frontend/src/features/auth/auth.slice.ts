import { createSlice } from "@reduxjs/toolkit";
import IAuthState from "../../models/auths/IAuthState";
import { login, register } from "./auth.thunk";

const initialState: IAuthState = {
    user: null,
    isAuthenticated: false,
    isLoading: false,
    error: null,
};

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        logout: (state) => {
            state.user = null;
            state.isAuthenticated = false;
        }
    },
    extraReducers: (builder) => {
        builder.addCase(login.pending, (state) => {
            state.isLoading = true;
            state.error = null;
        }).addCase(login.fulfilled, (state, action) => {
            state.isLoading = false;
            state.user = action.payload as any;
            state.isAuthenticated = true;
        }).addCase(login.rejected, (state, action) => {
            state.isLoading = false;
            state.error = action.payload as any;
        }).addCase(register.pending, (state) => {
            state.isLoading = true;
            state.error = null;
        }).addCase(register.fulfilled, (state, action) => {
            state.user = action.payload as any;
            state.isAuthenticated = true;
        }).addCase(register.rejected, (state, action) => {
            state.isLoading = false;
            state.error = action.payload as any;
        })
    },
});

export const { logout } = authSlice.actions;
export default authSlice.reducer;