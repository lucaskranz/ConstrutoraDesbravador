export interface PaginacaoResult<T> {
    items: T[];       
    total: number;    
    totalPages: number;  
    page: number;     
    size: number;     
}