using Finance.Application.Clinents.Commands.CreateClient;
using Finance.Application.FinancialAccountBalance.Commands.AddMoneyToBalanceByClient;
using Finance.Application.FinancialAccountBalance.Commands.DeductMoneyToBalanceByClient;
using Finance.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using Finance.Domain;
using Finance.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Concurrent;

using Finance.Persistence;

using Xunit;

namespace Finance.Tests.Finance
{
    public class ChanginTheBalanceCommandHandlerTests : UnitOfWork
    {

        readonly int maxThreads = 10;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        
        [Fact]
        public async void CreateClientCommandHandler_Success()
        {
            //Arrange

            //регистрация 50 пользователей
            var registeredUsers = await RegisterUsers(50);
            var handlerAddFinancialAccount = new CreateFinancialAccountCommandsHandler(_context);
            
            var options = new DbContextOptionsBuilder<FinanceDbContext>()
                .Options;

            var contextFactory = new FinanceContextFactory(options);
            
            //Act
            foreach (var registeredUser in registeredUsers)
            {
                await handlerAddFinancialAccount.Handle(
                        new CreateFinancialAccountCommand(registeredUser),
                        CancellationToken.None);
            }

            var clients = _context.Clients.ToList();
            var accounts = _context.FinancialAccounts.ToList();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(@"E:\workingZone\logging\log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            
            using var semaphore = new SemaphoreSlim(10);

            List<Task> tasks = new List<Task>();

            foreach (var clientId in clients)
            {
                for (int i = 0; i < 10; i++)
                {
                    // Запускаем задачу
                    tasks.Add(Task.Run(async () =>
                    {
                        await semaphore.WaitAsync();

                        try
                        {
                            await AddAndRemoveMoneyToAccount(contextFactory, clientId.Id);
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    }));
                }
            }

            // Ждем завершения всех задач
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

        private void UpdateAccountBalance(List<Client> clients)
        {
            var lockObjectForAdd = new object();
            var lockObjectForDeduct = new object();
            var handlerAddMoney = new AddMoneyToFinancialAccountCommandHandler(_context);
            var handlerDeductMoney = new DeductMoneyToFinancialAccountCommandHandler(_context);

            
        }

        private async Task AddAndRemoveMoneyToAccount(FinanceContextFactory contextFactory, Guid clientId)
        {
            var context = contextFactory.CreateDbContext();
            
            var handlerAddMoney = new AddMoneyToFinancialAccountCommandHandler(context);
            var handlerDeductMoney = new DeductMoneyToFinancialAccountCommandHandler(context);
            
            
            await handlerDeductMoney.Handle(new DeductMoneyToFinancialAccountCommand
                { Balance = 5, ClientId = clientId }, CancellationToken.None);
            
            await handlerAddMoney.Handle(new AddMoneyToFinancialAccountCommand
                { Balance = 10, ClientId = clientId }, CancellationToken.None);
            
            
        }

        
        public async Task<List<Guid>> RegisterUsers(int quantity)
        {
            var handler = new CreateClientCommandsHandler(_context, Mediator);
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
