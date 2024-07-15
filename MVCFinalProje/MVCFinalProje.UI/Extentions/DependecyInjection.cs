using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCFinalProje.Infrastructure.AppContext;
using MVCFinalProje.Infrastructure.Repositories.AuthorRepository;
using MVCFinalProje.Infrastructure.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Infrastructure.Extentions
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddUIServices(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
        
    }
}
