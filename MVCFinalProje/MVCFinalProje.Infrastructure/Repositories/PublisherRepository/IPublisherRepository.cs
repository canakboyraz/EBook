using MVCFinalProje.Domain.Entities;
using MVCFinalProje.Infrastructure.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Repositories.PublisherRepository
{
    public interface IPublisherRepository : IAsyncRepository, IAsyncFindable<Publisher>, IAsyncInsertable<Publisher>, IAsyncQueryableRepository<Publisher>, IAsyncDeletableRepository<Publisher>, IAsyncUpdatebleRepository<Publisher>,IAsyncOrderableRepository<Publisher>
    {
    }
}
