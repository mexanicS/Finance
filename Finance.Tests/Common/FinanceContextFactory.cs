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
using Microsoft.Data.Sqlite;

namespace Finance.Tests.Common
{
    public class FinanceContextFactory
    {
        private readonly SqliteConnection _connection;
        public static Guid UserAId { get; set; } = Guid.NewGuid();
        public FinanceContextFactory(SqliteConnection connection)
        {
            _connection = connection;
        }

        public FinanceDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<FinanceDbContext>()
                .UseSqlite(_connection) // Используем переданное подключение
                .Options;

            var context = new FinanceDbContext(options);
            context.Database.EnsureCreated(); // Создаем базу данных, если она еще не создана

            return context;
        }
    }
}
