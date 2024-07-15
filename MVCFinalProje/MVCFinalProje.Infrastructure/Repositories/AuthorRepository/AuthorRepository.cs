using Microsoft.EntityFrameworkCore;
using MVCFinalProje.Core.Interfaces;
using MVCFinalProje.Domain.Entities;
using MVCFinalProje.Infrastructure.AppContext;
using MVCFinalProje.Infrastructure.DataAccess.EntityFremework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Repositories.AuthorRepository
{
    internal class AuthorRepository : EFBaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }
    }
}
