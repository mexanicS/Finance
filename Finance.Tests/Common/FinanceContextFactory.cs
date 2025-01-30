using Finance.Domain;
using Finance.Persistence;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Tests.Common
{
    public class FinanceContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();

        private readonly DbContextOptions<FinanceDbContext> _options;

        public FinanceContextFactory(DbContextOptions<FinanceDbContext> options)
        {
            _options = options;
        }

        public FinanceDbContext CreateDbContext()
        {
            return new FinanceDbContext(_options);
        }
    }
}
