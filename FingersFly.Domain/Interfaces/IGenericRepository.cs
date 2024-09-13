using FingersFly.Domain.Entities;

namespace FingersFly.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int Id);
        Task<T?> GetWithSpecAsync(ISpecification<T> spec);
        Task<TResult?> GetWithSpecAsync<TResult>(ISpecification<T, TResult> spec);

        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<TResult>> ListWithSpecAsync<TResult>(ISpecification<T, TResult> spec);

        void Add(T Entity);
        void Update(T Entity);
        void Delete(int Id);
        Task<bool> SaveAllAsync();
        bool Exist(int Id);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
