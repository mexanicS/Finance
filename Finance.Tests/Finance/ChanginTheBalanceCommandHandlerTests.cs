using Finance.Application;
using Finance.Application.Clinents.Commands.CreateClient;
using Finance.Application.FinancialAccountBalance.Commands.AddMoneyToBalanceByClient;
using Finance.Application.FinancialAccountBalance.Commands.DeductMoneyToBalanceByClient;
using Finance.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using Finance.Domain;
using Finance.Persistence;
using Finance.Tests.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Finance.Tests.Finance
{
    public class ChanginTheBalanceCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateClientCommandHandler_Success()
        {
            //Arrange
            int maxDegreeOfParallelism = 10;

            //регистрация 50 пользователей
            var registeredUsers = await RegisterUsers(50);
            var handlerAddFinancialAccount = new CreateFinancialAccountCommandsHandler(Context);
            var handlerAddMoney = new AddMoneyToFinancialAccountCommandHandler(Context);
            var handlerDeductMoney = new DeductMoneyToFinancialAccountCommandHandler(Context);

            //Act
            foreach (var registeredUser in registeredUsers)
            {
                await handlerAddFinancialAccount.Handle(
                        new CreateFinancialAccountCommand(registeredUser),
                        CancellationToken.None);
            }

            var semaphore = new SemaphoreSlim(maxDegreeOfParallelism);

            var tasks = registeredUsers.Select(async client =>
            {
                await semaphore.WaitAsync();
                try
                {
                    await handlerAddMoney.Handle(new AddMoneyToFinancialAccountCommand
                    { Balance = 15, ClientId = client }, CancellationToken.None);

                    await handlerDeductMoney.Handle(new DeductMoneyToFinancialAccountCommand
                    { Balance = 7, ClientId = client }, CancellationToken.None);
                }
                finally
                {
                    semaphore.Release();
                }
            }).ToArray();

            await Task.WhenAll(tasks);

            //Assert
            foreach (var clientId in registeredUsers)
            {
                Assert.NotNull(
                await Context.FinancialAccounts.FirstOrDefaultAsync(financialAccounts =>
                    financialAccounts.ClientId == clientId && financialAccounts.Balance == 8));
            }
        }

        public async Task<List<Guid>> RegisterUsers(int quantity)
        {
            var handler = new CreateClientCommandsHandler(Context, Mediator);
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
