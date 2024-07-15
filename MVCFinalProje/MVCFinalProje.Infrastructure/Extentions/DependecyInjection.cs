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
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetConnectionString("AppConnectionString"));
            });
            services.AddScoped<IAuthorRepository, AuthorRepository>();


            // Seed Data ( genelde mig işlemlerinde yprum satırına almamız gerekebilir. )

            AdminSeed.SeedAsync(configuration).GetAwaiter().GetResult();



            return services;
        }
    }
}
