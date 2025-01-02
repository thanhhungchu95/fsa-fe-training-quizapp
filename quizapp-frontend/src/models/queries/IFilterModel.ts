import OrderDirectionEnum from "../../core/enums/OrderDirectionEnum";

export default interface IFilterModel {
    page: number;
    size: number;
    orderBy: string;
    orderDirection: OrderDirectionEnum;
    isActive: boolean;
}