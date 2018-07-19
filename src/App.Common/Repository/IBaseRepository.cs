using System.Threading.Tasks;

namespace App.Common.Repository
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task<bool> AddAsync(TEntity value);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(TEntity value);
        Task<TEntity> GetAsync(int id);

    }
}