using MVCFinalProje.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncQueryableRepository<TEntity> where TEntity: BaseEntity // çoklu sorgulama
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true); // hepsini getir çoklu sorgu
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true); // koşula göre getir. Overload çoklu sorgu
    }
}
