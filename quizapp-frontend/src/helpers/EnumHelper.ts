const convertEnumToArray = (enumObject: any) => {
    return Object.keys(enumObject).map((key, index) => ({ key: index, value: enumObject[key] }));
}

const getDisplayValue = (enumObject: any, key: any) => {
    return enumObject[key];
}

export const EnumHelper = {
    convertEnumToArray,
    getDisplayValue,
};