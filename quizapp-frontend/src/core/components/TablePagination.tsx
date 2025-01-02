import { faEdit, faTrash, faAngleDoubleLeft, faAngleLeft, faAngleRight, faAngleDoubleRight } from "@fortawesome/free-solid-svg-icons";
import ITablePaginationProps from "../../models/ITablePaginationProps";
import { useEffect, useState } from "react";
import OrderDirectionEnum from "../enums/OrderDirectionEnum";
import TableColumnModel from "../../models/TableColumnModel";
import IconButton from "../../shared/components/wrappers/IconButton";
import { EnumHelper } from "../../helpers/EnumHelper";
import { FormatHelper } from "../../helpers/FormatHelper";

const TablePagination: React.FC<ITablePaginationProps> = ({ items, pageInfo, defaultOrderBy, columns, onEdit, onDelete, onSearch }) => {
    const [orderBy, setOrderBy] = useState<string>(defaultOrderBy);
    const [orderDirection, setOrderDirection] = useState<OrderDirectionEnum>(OrderDirectionEnum.Ascending);
    const [page, setPage] = useState<number>(1);
    const [size, setSize] = useState<number>(5);
    const [pageLimit] = useState<number>(3);
    const [pageSizeList] = useState<number[]>([5, 10, 20, 50, 100,]);
    const defaultPaginationClassName = "w-8 h-8 flex justify-center items-center rounded-full border border-slate-300";

    useEffect(() => {
        onSearch(page, size, orderBy, orderDirection);
    }, [page, size, orderBy, orderDirection, onSearch]);

    const calculatePage = (): number[] => {
        let start: number = Math.max(1, page - pageLimit);
        let end: number = Math.min(pageInfo.totalPages, page + pageLimit);
        return Array(end - start + 1).fill(0).map((_, i) => i + start);
    }

    const orderByField = (field: string) => {
        setOrderBy(field);
        setOrderDirection(orderBy === field && orderDirection === OrderDirectionEnum.Descending ? 0 : 1);
    }

    const getDisplayValueByType = (column: TableColumnModel, item: any): any => {
        switch (column.type) {
            case 'boolean':
                return item[column.field] ? 'Yes' : 'No';
            case 'enum':
                return EnumHelper.getDisplayValue(column.enum, item[column.field]);
            case 'duration':
                return FormatHelper.durationConversionFromMinutes(item[column.field]);
            default:
                return item[column.field];
        }
    }

    return (
        <div className="card border border-slate-300 rounded-md my-4">
            <div className="card-body p-3 border-y border-slate-300">
                <table className="w-full">
                    <thead>
                        <tr className="*:border *:border-slate-300 *:p-3">
                            <th>No.</th>
                            {
                                columns.map((column: TableColumnModel) => (
                                    <th key={column.field}>
                                        {column.sortabled ? (
                                            <IconButton onClick={() => orderByField(column.field)} text={column.label}
                                                icon={orderDirection === OrderDirectionEnum.Ascending && orderBy === column.field ? column.iconSort[OrderDirectionEnum.Descending] : column.iconSort[OrderDirectionEnum.Ascending]}  />
                                        ) : column.label
                                        }
                                    </th>
                                ))
                            }
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {items.length !== 0 && items.map((item, index) => (
                            <tr key={item.id} className="*:border *:border-slate-300 *:p-3">
                                <td>
                                    {pageInfo.pageSize * (pageInfo.pageIndex - 1) + index + 1}
                                </td>
                                {
                                    columns.map((column: TableColumnModel) => (
                                        <td key={column.field}>
                                            {getDisplayValueByType(column, item)}
                                        </td>
                                    ))
                                }
                                <td>
                                    <div className="flex justify-center space-x-3">
                                        <IconButton icon={faEdit} className={"text-blue-500"} onClick={() => onEdit(item)} />
                                        <IconButton icon={faTrash} className={"text-red-500"} onClick={() => onDelete(item)} />
                                    </div>
                                </td>
                            </tr>
                        ))}
                        {
                            items.length === 0 && (
                                <tr>
                                    <td colSpan={columns.length + 2} className="text-lg text-center">No data to display</td>
                                </tr>
                            )
                        }
                    </tbody>
                </table>
            </div>
            <div className="card-footer p-3 flex justify-between">
                <div className="select-page-size flex items-center">
                    <label htmlFor="pageSize" className="block mr-2">Items per page: </label>
                    <select name="pageSize" id="pageSize" onChange={(e) => setSize(parseInt(e.target.value))} value={size} title="Select Page Size"
                        className="p-2 border border-slate-300 rounded-sm">
                        {
                            pageSizeList.map((item) => (
                                <option key={`page-size-${item}`} value={item}>{item}</option>
                            ))
                        }
                    </select>
                </div>
                <div className="list-page flex items-center space-x-3">
                    <IconButton disabled={page === 1} onClick={() => setPage(1)} icon={faAngleDoubleLeft} title={"First Page"}
                        className={`${defaultPaginationClassName} ${page === 1 ? 'cursor-not-allowed' : ''}`} iconClassName={page === 1 ? 'text-slate-400' : 'text-blue-500'} />
                    <IconButton disabled={page === 1} onClick={() => setPage(page - 1)} icon={faAngleLeft} title={"Previous Page"}
                        className={`${defaultPaginationClassName} ${page === 1 ? 'cursor-not-allowed' : ''}`} iconClassName={page === 1 ? 'text-slate-400' : 'text-blue-500'} />

                    {calculatePage().map((item) => (
                        <button key={item} type="button" onClick={() => setPage(item)} title={`Page ${item}`}
                            className={`${defaultPaginationClassName} text-blue-500 ${page === item ? 'bg-blue-500 text-white' : ''}`}>
                            {item}
                        </button>))}

                    <IconButton disabled={page === pageInfo.totalPages} onClick={() => setPage(page + 1)} icon={faAngleRight} title={"Previous Page"}
                        className={`${defaultPaginationClassName} ${page === pageInfo.totalPages ? 'cursor-not-allowed' : ''}`} iconClassName={page === pageInfo.totalPages ? 'text-slate-400' : 'text-blue-500'} />
                    <IconButton disabled={page === pageInfo.totalPages} onClick={() => setPage(pageInfo.totalPages)} icon={faAngleDoubleRight} title={"Last Page"}
                        className={`${defaultPaginationClassName} ${page === pageInfo.totalPages ? 'cursor-not-allowed' : ''}`} iconClassName={page === pageInfo.totalPages ? 'text-slate-400' : 'text-blue-500'} />
                </div>
                <div className="page-info">
                    {pageInfo && `${pageInfo.totalItems === 0 ? 0 : pageInfo.pageSize * (pageInfo.pageIndex - 1) + 1}-${Math.min(pageInfo.pageSize * pageInfo.pageIndex, pageInfo.totalItems)} of ${pageInfo.totalItems}`}
                </div>
            </div>
        </div>
    );
}

export default TablePagination;