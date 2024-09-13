﻿namespace FingersFly.Domain.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int Index { get; set; } = 1;

        private int _pageSize = 8;
        public int PageSize
        {
            get => _pageSize; 
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        private List<string> _brands = [];
        public List<string> Brands
        {
            get => _brands;
            set => _brands = value
                .SelectMany(x => x.Split(",", StringSplitOptions.RemoveEmptyEntries))
                .ToList();
        }

        private List<string> _types = [];
        public List<string> Types
        {
            get => _types;
            set => _types = value
                .SelectMany(x => x.Split(",", StringSplitOptions.RemoveEmptyEntries))
                .ToList();
        }

        private string? _search;
        public string Search
        {
            get => _search ?? "";
            set => _search = value.ToLower();
        }

        public string? SortColumn { get; set; }
        public string? SortType { get; set; }
    }
}
