using FingersFly.Domain.Entities;
using FingersFly.Domain.Specifications;

namespace FingersFly.Domain.Interfaces
{
    public interface IProductRepo
    {
        Task<IReadOnlyList<Product>> GetProducts(ProductSpec spec);
        Task<Product> GetProductById(int Id);
        Task Update(Product product);
        Task Delete(int Id);
        Task<Product> Add(Product product);
        Task<IReadOnlyList<string>> GetBrands();
        Task<IReadOnlyList<string>> GetTypes();
        Task<int> Count();
    }
}
