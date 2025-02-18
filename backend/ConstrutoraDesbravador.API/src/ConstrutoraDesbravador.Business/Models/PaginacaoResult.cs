﻿namespace ConstrutoraDesbravador.Business.Models
{
    public class PaginacaoResult<T>
    {
        public IEnumerable<T> Items { get; set; } 
        public int Total { get; set; } 
        public int Page { get; set; } 
        public int Size { get; set; } 
        public int TotalPages => (int)Math.Ceiling((double)Total / Size); 
    }    
}
