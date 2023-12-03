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
        private FinanceDbContext _context;

        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public FinanceDbContext CreateContext()
        {
            if (_context == null)
            {
                var options = new DbContextOptionsBuilder<FinanceDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                _context = new FinanceDbContext(options);
                _context.Database.EnsureCreated();
            }

            return _context;
        }

        public static void Destroy(FinanceDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
