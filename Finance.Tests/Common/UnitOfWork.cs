using Finance.Domain;
using Finance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Tests.Common
{
    public class YourRepository
    {
        private readonly FinanceDbContext _context;

        public YourRepository(FinanceDbContext context)
        {
            _context = context;
        }

        public List<FinancialAccount> GetFinancialAccount()
        {
            var data = _context.FinancialAccounts.ToList();

            return data;
        }

        public List<Client> GetClients()
        {
            var data = _context.Clients.ToList();

            return data;
        }

        public Guid AddClient(Client client)
        {
            _context.Clients.Add(client);

            _context.SaveChanges();

            return client.Id;
        }

        // Реализация методов для доступа к сущностям базы данных
    }

    public class UnitOfWork : IDisposable
    {
        protected readonly FinanceDbContext _context;
        protected readonly IMediator Mediator;

        public YourRepository YourRepository { get; }

        public UnitOfWork()
        {
            var options = new DbContextOptionsBuilder<FinanceDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            _context = new FinanceDbContext(options);
            YourRepository = new YourRepository(_context);
            Mediator = Substitute.For<IMediator>();
        }
        public List<FinancialAccount> GetFinancialAccount()
        {
            return YourRepository.GetFinancialAccount();
        }

        public Guid AddClient(Client client)
        {
            return YourRepository.AddClient(client);
        }
        public List<Client> GetClients()
        {
            return YourRepository.GetClients();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
