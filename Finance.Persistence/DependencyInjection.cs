using Finance.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContextPool<FinanceDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<IFinanceDbContext>(provider =>
                provider.GetService<FinanceDbContext>());

            return services;
        }
    }
}
