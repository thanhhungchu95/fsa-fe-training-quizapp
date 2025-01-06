import { UserViewModel } from "../../view-models/user/UserViewModel";

export default interface IAuthState {
    user: UserViewModel | undefined | null | unknown;
    isAuthenticated: boolean;
    isLoading: boolean;
    error: any;
}