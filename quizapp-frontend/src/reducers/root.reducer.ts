import { combineReducers } from "@reduxjs/toolkit";
import authReduder from "../features/auth/auth.slice";

const rootReducer = combineReducers({
    auth: authReduder,
});

export type RootState = ReturnType<typeof rootReducer>;
export default rootReducer;