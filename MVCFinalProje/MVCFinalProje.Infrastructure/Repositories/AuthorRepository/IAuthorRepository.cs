using MVCFinalProje.Domain.Entities;
using MVCFinalProje.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Repositories.AuthorRepository
{
    public interface IAuthorRepository : IAsyncRepository, IAsyncFindable<Author>,IAsyncInsertable<Author> , IAsyncQueryableRepository<Author>, IAsyncDeletableRepository<Author>, IAsyncUpdatebleRepository<Author>// Burada IAsyncFindable teyit et hata olabilir.
    {

    }
}
