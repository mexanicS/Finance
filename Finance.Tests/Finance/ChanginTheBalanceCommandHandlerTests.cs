using Finance.Application.Clinents.Commands.CreateClient;
using Finance.Application.FinancialAccountBalance.Commands.DeductMoneyToBalanceByClient;
using Finance.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using Finance.Domain;
using Finance.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Concurrent;
using Finance.Application.FinancialAccountBalance.Commands.AddMoneyToFinancialAccount;
using Finance.Persistence;
using MediatR;
using Microsoft.Data.Sqlite;
using Xunit;

namespace Finance.Tests.Finance
{
    public class ChanginTheBalanceCommandHandlerTests : UnitOfWork
    {

        readonly int maxThreads = 10;
        //private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        
        [Fact]
        public async void CreateClientCommandHandler_Success()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            //Arrange
            
            var contextFactory = new FinanceContextFactory(connection);
            var _context = contextFactory.CreateDbContext();
            //регистрация 50 пользователей
            var registeredUsers = await RegisterUsers(50, _context);
            var handlerAddFinancialAccount = new CreateFinancialAccountCommandsHandler(_context);
            
            //Act
            foreach (var registeredUser in registeredUsers)
            {
                await handlerAddFinancialAccount.Handle(
                        new CreateFinancialAccountCommand(registeredUser),
                        CancellationToken.None);
            }

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(@"F:\Projects\logger\log.txt", rollingInterval: RollingInterval.Minute)
                .CreateLogger();

            var tasks = new List<Task>();

            for (var i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    // Запускаем оба метода параллельно
                    await Task.WhenAll(
                        AddMoneyToAccount(contextFactory),
                        DeductMoneyToAccount(contextFactory)
                    );
                }));
            }

            await Task.WhenAll(tasks);

            var accountsNew = _context.FinancialAccounts.ToList();

            foreach (var acc in accountsNew)
            {
                Log.Information($"На акк: {acc.ClientId}. денег стока {acc.Balance} ");
            }

            Log.CloseAndFlush();

            foreach (var clientId in registeredUsers)
            {
                Assert.NotNull(
                await _context.FinancialAccounts.FirstOrDefaultAsync(financialAccounts =>
                    financialAccounts.ClientId == clientId && financialAccounts.Balance == 50));
            }
        }

        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private async Task AddMoneyToAccount(FinanceContextFactory contextFactory)
        {
            await using var context = contextFactory.CreateDbContext();
            var handlerAddMoney = new AddMoneyToFinancialAccountCommandHandler(context);

            var clientIds = await context.Clients.Select(x => x.Id).ToListAsync();

            foreach (var clientId in clientIds)
            {
                await _semaphore.WaitAsync();
                try
                {
                    await handlerAddMoney.Handle(new AddMoneyToFinancialAccountCommand
                    {
                        Balance = 10,
                        ClientId = clientId
                    }, CancellationToken.None);
                }
                finally
                {
                    _semaphore.Release();
                }
            }
        }

        private async Task DeductMoneyToAccount(FinanceContextFactory contextFactory)
        {
            await using var context = contextFactory.CreateDbContext();
            var handlerDeductMoney = new DeductMoneyToFinancialAccountCommandHandler(context);

            var clientIds = await context.Clients.Select(x => x.Id).ToListAsync();

            foreach (var clientId in clientIds)
            {
                await _semaphore.WaitAsync();
                try
                {
                    await handlerDeductMoney.Handle(new DeductMoneyToFinancialAccountCommand
                    {
                        Balance = 5,
                        ClientId = clientId
                    }, CancellationToken.None);
                }
                finally
                {
                    _semaphore.Release();
                }
            }
        }
        
        public async Task<List<Guid>> RegisterUsers(int quantity, FinanceDbContext context)
        {
            var handler = new CreateClientCommandsHandler(context, Mediator);
            List<Guid> listClientsId = new();
            for (int i = 0; i < quantity; i++)
            {

                var firstName = "user" + i;
                var lastName = "lasting" + i;
                var middleName = "mid" + i;
                var dateOfBirth = new DateTime(2006, 11, 29, 12, 0, 0);

                var clientId = await handler.Handle(
                    new CreateClientCommand
                    {
                        DateOfBirth = dateOfBirth,
                        FirstName = firstName,
                        LastName = lastName,
                        MiddleName = middleName,
                        UserId = FinanceContextFactory.UserAId
                    },
                    CancellationToken.None);
                listClientsId.Add(clientId);
            }
            return listClientsId;
        }

    }
}
