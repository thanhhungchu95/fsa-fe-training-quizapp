import { useCallback, useEffect, useRef, useState } from "react";
import TableColumnModel from "../../../models/TableColumnModel";
import { faPlus, faRefresh, faSearch, faSortAlphaAsc, faSortAlphaDesc } from "@fortawesome/free-solid-svg-icons";
import { BaseApiService } from "../../../services/apis/base.service";
import { RoleViewModel } from "../../../view-models/role/RoleViewModel";
import PageInfo from "../../../models/PageInfo";
import OrderDirectionEnum from "../../../core/enums/OrderDirectionEnum";
import IFilterModel from "../../../models/queries/IFilterModel";
import { RoleService } from "../../../services/apis/role.service";
import Loading from "../../../core/components/Loading";
import IconButton from "../../../shared/components/wrappers/IconButton";
import TablePagination from "../../../core/components/TablePagination";
import RoleDetails from "./RoleDetails";
import Checkbox from "../../../core/components/Checkbox";

const RoleList = () => {
    const [items, setItems] = useState<RoleViewModel[]>([]); 
    const [loading, setLoading] = useState<boolean>(true);
    const [pageInfo, setPageInfo] = useState<PageInfo>(new PageInfo());
    const [page, setPage] = useState<number>(1);
    const [size, setSize] = useState<number>(5);
    const [orderBy, setOrderBy] = useState<string>('name');
    const [orderDirection, setOrderDirection] = useState<number>(OrderDirectionEnum.Ascending);
    const [keyword, setKeyword] = useState<string>('');
    const [isActive, setIsActive] = useState<boolean>(true);
    const [isShowDetail, setIsShowDetail] = useState<boolean>(false);
    const [selectedItem, setSelectedItem] = useState<any>(null);
    const detailForm = useRef<HTMLDivElement>(null);

    const columns: TableColumnModel[] = [
        { field: 'name', label: 'Name', type: 'string', sortabled: true, iconSort: [faSortAlphaAsc, faSortAlphaDesc], enum: null },
        { field: 'description', label: 'Description', type: 'string', sortabled: true, iconSort: [faSortAlphaAsc, faSortAlphaDesc], enum: null },
        { field: 'isActive', label: 'Active?', type: 'boolean', sortabled: true, iconSort: [faSortAlphaAsc, faSortAlphaDesc], enum: null },
    ];

    const searchData = useCallback(async() => {
        try {
            const filter: IFilterModel = {
                page: page,
                size: size,
                orderBy: orderBy,
                orderDirection: orderDirection,
                isActive: isActive,
            };

            if (keyword) {
                Object.assign(filter, { name: keyword });
            }

            const response: any = await RoleService.search(filter);

            if (response) {
                setInterval(() => {
                    setLoading(false);
                }, 0);
            }

            setItems(response.items);
            setPageInfo(response.pageInfo);
        } catch (error) {
            BaseApiService.handleError(error);
        }
    }, [page, size, orderBy, orderDirection, keyword, isActive]);

    const handleSubmit = async (e: any) => {
        e.preventDefault();
        searchData();
    };

    const onSearch = (page: number, size: number, orderBy: string, orderDirection: OrderDirectionEnum) => {
        setPage(page);
        setSize(size);
        setOrderBy(orderBy);
        setOrderDirection(orderDirection);
    }

    const onCreate = () => {
        setIsShowDetail(false);
        setSelectedItem(null);
        setTimeout(() => {
            setIsShowDetail(true);
            // Auto focus on detail form
            detailForm.current?.scrollIntoView({ behavior: 'smooth' });
        });
    };

    const onEdit = (item: any) => {
        setIsShowDetail(false);
        setSelectedItem(item);
        setTimeout(() => {
            setIsShowDetail(true);
            // Auto focus on detail form
            detailForm.current?.scrollIntoView({ behavior: 'smooth' });
        });
    }

    const onDelete = async (item: any) => {
        let response: any;
        try {
            response = await RoleService.remove(item.id);
        } catch (error) {
            BaseApiService.handleError(error);
        }
        if (response) {
            searchData();
        }
    };
    const onCancelDetail = () => {
        setIsShowDetail(false);
        setSelectedItem(null);
        searchData();
    };

    const onClear = () => {
        setKeyword('');
        setIsActive(true);
    }

    useEffect(() => {
        searchData();
    }, [size, page, orderBy, orderDirection, searchData]);

    if (loading) return <Loading />;

    return (
        <section className="w-5/6">
            <div className="card border border-slate-300 rounded-md">
                <div className="card-header p-3">
                    <h1 className="text-2xl font-semibold">Role Management</h1>
                </div>
                <form onSubmit={handleSubmit}>
                    <div className="card-body p-3 border-y border-slate-300 flex flex-row">
                        <div className="form-group w-full">
                            <label htmlFor="title" className="block mb-3">Keyword</label>
                            <input type="text" id="title" name="title" onChange={(e) => setKeyword(e.target.value)}
                                className="p-2 border border-slate-300 rounded-sm w-full" />
                        </div>
                        <div className="form-group w-1/6 flex flex-col ml-4">
                            <label htmlFor="isActive" className="block mb-3">Active?</label>
                            <Checkbox title={"isActive"} onChange={() => setIsActive(!isActive)} checked={isActive} />
                        </div>
                    </div>
                    <div className="card-footer p-3 flex justify-between">
                        <IconButton className={"p-2 px-4 bg-green-500 text-white hover:bg-green-700 rounded-full"}
                            onClick={onCreate} icon={faPlus} iconClassName={"mr-2"} text={"Create"} />
                        <div className="search-actions space-x-3">
                            <IconButton type={"reset"} className={"p-2 px-4 bg-slate-300 text-white hover:bg-slate-500 rounded-full"}
                                icon={faRefresh} iconClassName={"mr-2"} onClick={onClear} text={"Clear"} /> 
                            <IconButton type={"submit"} className={"p-2 px-4 bg-blue-500 text-white hover:bg-blue-700 rounded-full"}
                                icon={faSearch} iconClassName={"mr-2"} text={"Search"} />
                        </div>
                    </div>
                </form>
            </div>

            {/* Table List With Paging */}
            <TablePagination items={items} defaultOrderBy={'name'} pageInfo={pageInfo} columns={columns} onEdit={onEdit} onDelete={onDelete} onSearch={onSearch} />

            {/* Details Component */}
            <div id="detail-form" ref={detailForm}>
                {isShowDetail && (<RoleDetails item={selectedItem} onCancel={() => onCancelDetail()} />)}
            </div>
        </section>
    );
}

export default RoleList;