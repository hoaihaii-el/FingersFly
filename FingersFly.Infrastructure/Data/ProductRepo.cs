using FingersFly.Domain.Entities;
using FingersFly.Domain.Exceptions;
using FingersFly.Domain.Interfaces;
using FingersFly.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FingersFly.Infrastructure.Data
{
    public class ProductRepo(StoreContext _context) : IProductRepo
    {
        public async Task<Product> Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product; 
        }

        public async Task<int> Count()
        {
            return await _context.Products.CountAsync();
        }

        public async Task Delete(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            _context.Remove(product ?? throw new NoActionException());
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<string>> GetBrands()
        {
            return await _context.Products.Select(p => p.Brand).ToListAsync();
        }

        public async Task<Product> GetProductById(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            return product ?? throw new EntityNotFoundException();
        }

        public async Task<IReadOnlyList<Product>> GetProducts(ProductSpec spec)
        {
            var products = _context.Products
                .Where(p =>
                    (string.IsNullOrEmpty(spec.Search) || p.Name.ToLower().Contains(spec.Search))
                 && (!spec.Brands.Any() || spec.Brands.Contains(p.Brand))
                 && (!spec.Types.Any() || spec.Types.Contains(p.Type)))
                .AsNoTracking()
                .AsQueryable();

            Expression<Func<Product, object>> keySelector = p => p.Id;
            if (!string.IsNullOrEmpty(spec.SortCol))
            {
                keySelector = spec.SortCol.ToLower() switch
                {
                    "name" => p => p.Name,
                    "price" => p => p.Price,
                    _ => p => p.Id
                };
            }

            if (spec.PageSize > 0)
            {
                products = products.Skip((spec.PageIndex - 1) * spec.PageSize).Take(spec.PageSize);
            }

            if (spec.SortType == null || spec.SortType.Contains("asc", StringComparison.OrdinalIgnoreCase))
            {
                products = products.OrderBy(keySelector);
            }
            else
            {
                products = products.OrderByDescending(keySelector);
            }

            return await products.ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetTypes()
        {
            return await _context.Products.Select(p => p.Type).ToListAsync();
        }

        public async Task Update(Product product)
        {
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
