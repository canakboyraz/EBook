using Microsoft.EntityFrameworkCore;
using MVCFinalProje.Domain.Entities;
using MVCFinalProje.Infrastructure.DataAccess.EntityFremework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Repositories.PublisherRepository
{
    public class PublisherRepository : EFBaseRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(DbContext context) : base(context)
        {
        }
    }
}
