using Finance.Application.Clinents.Commands.CreateClient;
using Finance.Application.FinancialAccountBalance.Commands.AddMoneyToBalanceByClient;
using Finance.Application.FinancialAccountBalance.Commands.DeductMoneyToBalanceByClient;
using Finance.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using Finance.Domain;
using Finance.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Concurrent;
using Xunit;

namespace Finance.Tests.Finance
{
    public class ChanginTheBalanceCommandHandlerTests : UnitOfWork
    {

        readonly int maxThreads = 10;
        [Fact]
        public async void CreateClientCommandHandler_Success()
        {
            //Arrange

            //регистрация 50 пользователей
            var registeredUsers = await RegisterUsers(50);
            var handlerAddFinancialAccount = new CreateFinancialAccountCommandsHandler(_context);
            
            
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
                .WriteTo.File(@"D:\logging\log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            
            List<Thread> threads = new();

            for (int i = 0; i < maxThreads; i++)
            {
                Thread thread = new(() => UpdateAccountBalance(clients));
                threads.Add(thread);
                thread.Start();
                //При отсутствии задержки пополнение и списание происходит максимально одновременно но не верно
                //но есть подозрения как будто данные не успевают сохранятся
                //тк в логе все операции происходят а в базе значения другие
                Thread.Sleep(200);

            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

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

            var partitioner = Partitioner.Create(clients, EnumerablePartitionerOptions.NoBuffering);
            Parallel.ForEach(partitioner, client =>
            {
                lock (lockObjectForAdd)
                {
                    int currentThreadId = Thread.CurrentThread.ManagedThreadId;

                    handlerAddMoney.Handle(new AddMoneyToFinancialAccountCommand
                    { Balance = 10, ClientId = client.Id }, CancellationToken.None);
                    
                    Log.Information($"В потоке: {currentThreadId}. На акк {client.Id} добавлено 10 единиц");
                }
            });
            Parallel.ForEach(partitioner, client =>
            {
                lock (lockObjectForDeduct)
                {
                    int currentThreadId = Thread.CurrentThread.ManagedThreadId;

                    handlerDeductMoney.Handle(new DeductMoneyToFinancialAccountCommand
                    { Balance = 5, ClientId = client.Id }, CancellationToken.None);

                    Log.Information($"В потоке: {currentThreadId}. С акк {client.Id} снято 5 единиц");
                }
            });
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
