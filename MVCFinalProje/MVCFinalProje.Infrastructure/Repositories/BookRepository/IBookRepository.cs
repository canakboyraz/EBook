using MVCFinalProje.Domain.Entities;
using MVCFinalProje.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Repositories.BookRepository
{
    public interface IBookRepository : IAsyncRepository, IAsyncFindable<Book>, IAsyncInsertable<Book>, IAsyncQueryableRepository<Book>, IAsyncDeletableRepository<Book>, IAsyncUpdatebleRepository<Book>
    {
    }
}
