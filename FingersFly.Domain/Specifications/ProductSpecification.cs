using FingersFly.Domain.Entities;
using System.Linq.Expressions;

namespace FingersFly.Domain.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(string? brand, string? type, string? sortCol, string? sortType) : base(x =>
            (string.IsNullOrEmpty(brand) || x.Brand.ToLower() == brand.ToLower())
         && (string.IsNullOrEmpty(type) || x.Type.ToLower() == type.ToLower()))
        {
            Expression<Func<Product, object>> keySelector = p => p.Id;
            if (!string.IsNullOrEmpty(sortCol))
            {
                keySelector = sortCol.ToLower() switch
                {
                    "name" => p => p.Name,
                    "price" => p => p.Price,
                    _ => p => p.Id
                };
            }

            if (string.IsNullOrEmpty(sortType))
            {
                AddOrderByDescending(keySelector);
                return;
            }

            if (!sortType.Contains("asc", StringComparison.OrdinalIgnoreCase))
            {
                AddOrderBy(keySelector);
            }
        }
    }
}
