using FingersFly.Domain.Entities;
using System.Linq.Expressions;

namespace FingersFly.Domain.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecParams specs) : base(x =>
            (string.IsNullOrEmpty(specs.Search) || x.Name.ToLower().Contains(specs.Search))
         && (!specs.Brands.Any() || specs.Brands.Contains(x.Brand))
         && (!specs.Types.Any() || specs.Types.Contains(x.Type)))
        {
            ApplyPaging(specs.PageSize * (specs.Index - 1), specs.PageSize);

            Expression<Func<Product, object>> keySelector = p => p.Id;
            if (!string.IsNullOrEmpty(specs.SortColumn))
            {
                keySelector = specs.SortColumn.ToLower() switch
                {
                    "name" => p => p.Name,
                    "price" => p => p.Price,
                    _ => p => p.Id
                };
            }

            if (string.IsNullOrEmpty(specs.SortType))
            {
                AddOrderByDescending(keySelector);
                return;
            }

            if (!specs.SortType.Contains("asc", StringComparison.OrdinalIgnoreCase))
            {
                AddOrderBy(keySelector);
            }
        }
    }
}
