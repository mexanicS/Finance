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
        public static Guid UserBId = Guid.NewGuid();

        public FinanceDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<FinanceDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new FinanceDbContext(options);
            context.Database.EnsureCreated();
            context.Clients.AddRange(
                new Client
                {
                    AddedDate = DateTime.Today,
                    Description = "Details1",
                    FirstName = "yertert",
                    Id = Guid.Parse("A6BB65BB-5AC2-4AFA-8A28-2616F675B825"),
                    LastName = "Title1",
                    UserId = UserAId,
                    MiddleName = "asd",
                    DateOfBirth = null
                });

            context.SaveChanges();
            return new FinanceDbContext(options);
        }

        public static void Destroy(FinanceDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
