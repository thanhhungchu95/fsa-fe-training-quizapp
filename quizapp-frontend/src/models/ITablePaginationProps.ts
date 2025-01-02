import OrderDirectionEnum from "../core/enums/OrderDirectionEnum";
import PageInfo from "./PageInfo";
import TableColumnModel from "./TableColumnModel";

export default interface ITablePaginationProps {
    pageInfo: PageInfo;
    items: any[];
    defaultOrderBy: string;
    columns: TableColumnModel[];
    onDelete: (item: any) => void;
    onEdit: (item: any) => void;
    onSearch: (page: number, size: number, orderBy: string, orderDirection: OrderDirectionEnum) => void;
}