using ClassLibrary.Dto.Page;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Entity;
using System.Linq.Expressions;

namespace ClassLibrary.Base
{
    public class BaseGenericRepository<T> : IBaseGenericRepository<T> where T : BaseEntity
    {
        protected readonly MyDbContext _myDbContext;
        public readonly DbSet<T> _dbSet;
        public BaseGenericRepository(MyDbContext myDbContext) {
            _myDbContext = myDbContext;
            _dbSet = _myDbContext.Set<T>();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="predicate">查询参数</param>
        /// <returns></returns>
        public async Task<PageData<T>> GetPaginatedAsync(BaseQueryPage page, Expression<Func<T, bool>> predicate = null) {
            var query = _dbSet.AsQueryable();

            if (predicate != null) {
                query = query.Where(predicate);
            }
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize)
                .ToListAsync();

            int totalPage = (int)Math.Ceiling((double)totalCount / page.PageSize);

            var pageData = new PageData<T>();
            pageData.CurrentPage = page.PageNumber;
            pageData.PageSize = page.PageSize;
            pageData.TotalPage = totalPage;
            pageData.Total = totalCount;
            pageData.Data = items;

            return pageData;
        }

        public async Task<PageData<T>> GetPaginatedAsync(IQueryable<T> queryAble, BaseQueryPage page) {
            var query = queryAble;

            var totalCount = await query.CountAsync();
            var items = await query
            .Skip((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize)
            .ToListAsync();

            int totalPage = (int)Math.Ceiling((double)totalCount / page.PageSize);

            var pageData = new PageData<T>();
            pageData.CurrentPage = page.PageNumber;
            pageData.PageSize = page.PageSize;
            pageData.TotalPage = totalPage;
            pageData.Total = totalCount;
            pageData.Data = items;

            return pageData;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync() {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// 通过ID获取指定数据
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public async Task<T?> GetByIdAsync(int id) {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(T entity) {
            _dbSet.Add(entity);
            var result = await _myDbContext.SaveChangesAsync();
            return result > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id) {
            var data = await GetByIdAsync(id);
            if (data == null) {
                return true;
            }
            _dbSet.Remove(data);
            return await _myDbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T entity) {
            _dbSet.Update(entity);

            return await _myDbContext.SaveChangesAsync() > 0;
        }

        
    }
}
