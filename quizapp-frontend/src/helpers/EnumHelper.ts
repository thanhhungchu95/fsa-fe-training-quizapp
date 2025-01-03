const convertEnumToArray = (enumObject: any) => {
    return Object.keys(enumObject)
        .filter((key) => isNaN(Number(key)) === false)
        .map((key, index) => ({ key: index, value: enumObject[key].replace(/([A-Z])/g, " $1") }));
}

const getDisplayValue = (enumObject: any, key: any) => {
    return enumObject[key];
}

export const EnumHelper = {
    convertEnumToArray,
    getDisplayValue,
};