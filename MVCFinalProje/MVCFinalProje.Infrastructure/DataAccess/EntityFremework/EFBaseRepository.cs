using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MVCFinalProje.Core.BaseEntities;
using MVCFinalProje.Core.Interfaces;
using MVCFinalProje.Enums;
using MVCFinalProje.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.DataAccess.EntityFremework
{
    public class EFBaseRepository<TEntity> : IRepository,IAsyncRepository,IAsyncInsertable<TEntity> , IAsyncUpdatebleRepository<TEntity> , IAsyncDeletableRepository<TEntity>, IAsyncFindable<TEntity> , IAsyncOrderableRepository<TEntity> , IAsyncQueryableRepository<TEntity>  where TEntity : BaseEntity
    {
        protected readonly DbContext _context; // protected kalıtım verdiğim yerlerde kullanabilir.
        protected readonly DbSet<TEntity> _table;

        public EFBaseRepository(DbContext context)
        {
            _context = context;
            _table = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entry = await _table.AddAsync(entity); // await Async metot için keyword zorunlu
            return entry.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) // await Async metot için keyword zorunlu
        {
            await _table.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> exception = null)
        {
            return  exception is null ? await GetAllActives().AnyAsync() : await GetAllActives().AnyAsync(exception); // eğer bir koşul varsa koşula göre sorgula -- Eğer yoksa tabloda herhangi bir true false varmı yok mu ona göre dön.
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.FromResult(_table.Remove(entity));
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities); // void döndüğü için SaveChangeAsync kullandık.
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool orderBySDesc, bool tracking = true)
        {
            return  orderBySDesc ? await GetAllActives(tracking).OrderByDescending(orderBy).ToListAsync() : await GetAllActives(tracking).OrderBy(orderBy).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderBy, bool orderBySDesc, bool tracking = true)
        {
            var values = GetAllActives(tracking).Where(expression); // Takip ve koşul durumu.
            return orderBySDesc ? await values.OrderByDescending(orderBy).ToListAsync() : await values.OrderBy(orderBy).ToListAsync(); // sıralama durumuna göre return ediyor.

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true)
        {
            return await GetAllActives(tracking).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
        {
            return await GetAllActives().Where(expression).ToListAsync();

        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> exception, bool tracking = true)
        {
            return await GetAllActives(tracking).FirstOrDefaultAsync(exception);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true)
        {
            return await GetAllActives(tracking).FirstOrDefaultAsync(x => x.Id == id);
        }

        public int SaveChange()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangeAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Task.FromResult(_table.Update(entity).Entity); // 
        }

        protected IQueryable<TEntity> GetAllActives(bool tracking = true)
        {
            var values = _table.Where(x => x.Status != Status.Deleted); // Statu su delete olanları getirmemek için metot yazıyoruz.
            return tracking ? values:values.AsNoTracking(); // Gelen veri tracking e dahil olmadan gelir. Takibe dahil olmaz.
        }



    }
}
