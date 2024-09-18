namespace FingersFly.Domain.Specifications
{
    public class ProductSpec
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _PageSize = -1;
        public int PageSize
        {
            get => _PageSize; 
            set => _PageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        private List<string> _Brands = [];
        public List<string> Brands
        {
            get => _Brands;
            set => _Brands = value
                .SelectMany(x => x.Split(",", StringSplitOptions.RemoveEmptyEntries))
                .ToList();
        }
        private List<string> _Types = [];
        public List<string> Types
        {
            get => _Types;
            set => _Types = value
                .SelectMany(x => x.Split(",", StringSplitOptions.RemoveEmptyEntries))
                .ToList();
        }
        public string? SortCol { get; set; }
        public string? SortType { get; set; }
        private string? _Search;
        public string Search
        {
            get => _Search ?? "";
            set => _Search = value.ToLower();
        }
    }
}
