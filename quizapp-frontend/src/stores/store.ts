import { configureStore } from "@reduxjs/toolkit";
import { LocalStorageHelper } from "../helpers/LocalStorageHelper";
import rootReducer from "../reducers/root.reducer";

const persistedState = LocalStorageHelper.loadState();

const store = configureStore({
    reducer: rootReducer,
    preloadedState: persistedState,
});

store.subscribe(() => {
    LocalStorageHelper.saveState(store);
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;