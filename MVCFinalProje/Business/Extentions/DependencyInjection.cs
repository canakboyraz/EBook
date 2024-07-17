using Microsoft.Extensions.DependencyInjection;
using MVCFinalProje.Business.Services.AuthorServices;
using MVCFinalProje.Business.Services.PublisherServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.Extentions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPublisherService, PublisherService>();
            return services;
        }
    }
}
