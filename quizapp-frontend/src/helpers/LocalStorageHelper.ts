const loadState = (): any => {
    const storeState = localStorage.getItem('reduxState');
    if (!storeState) return {};

    return JSON.parse(localStorage.getItem('reduxState') as string);
};

const saveState = (store: any) => {
    localStorage.setItem('reduxState', JSON.stringify(store.getState()));
};

const loadToken = (): string | null => {
    const storeState = loadState();
    return storeState?.auth?.user?.token || null;
};

export const LocalStorageHelper = {
    loadState,
    saveState,
    loadToken,
}