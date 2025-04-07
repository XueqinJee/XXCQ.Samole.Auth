using ClassLibrary.Dto.Page;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Base {
    public interface IBaseGenericRepository<T> where T : BaseEntity {
        Task<PageData<T>> GetPaginatedAsync(BaseQueryPage page, Expression<Func<T, bool>> predicate = null);
        Task<PageData<T>> GetPaginatedAsync(IQueryable<T> queryAble, BaseQueryPage page);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(T entity);
    }
}
