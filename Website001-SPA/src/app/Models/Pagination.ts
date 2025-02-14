export interface Pagination{
    pageSize:number;
    totalPages:number;
    count:number;
    pageNumber:number;
}

export class PaginationResult<T>{
    pagination:Pagination;
    result:T;
}