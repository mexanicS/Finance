using Finance.Application;
using Finance.Application.Clinents.Commands.CreateClient;
using Finance.Application.FinancialAccountBalance.Commands.AddMoneyToBalanceByClient;
using Finance.Application.FinancialAccountBalance.Commands.DeductMoneyToBalanceByClient;
using Finance.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using Finance.Domain;
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
            // Подготовка к тесту - регистрация 50 пользователей
            var registeredUsers = await RegisterUsers(50);

            var handlerAddFinancialAccount = new CreateFinancialAccountCommandsHandler(Context);

            foreach (var registeredUser in registeredUsers) 
            {
                await handlerAddFinancialAccount.Handle(
                        new CreateFinancialAccountCommand (registeredUser){},
                        CancellationToken.None);
            }



            // Создание коллекции задач для распараллеливания начисления/списания
            var tasks = new List<Task>();

            var handlerAddMoney = new AddMoneyToFinancialAccountCommandHandler(Context);
            var handlerDeductMoney = new DeductMoneyToFinancialAccountCommandHandler(Context);

            for (int i = 0; i < 10; i++)
            {
                var threadIndex = i;
                var task = Task.Run(async () =>
                {
                    var clients = Context.Clients.ToList();
                    
                    var clientsToProcess = clients.Skip(threadIndex * 5).Take(5);
                    foreach (var client in clientsToProcess) 
                    {
                        await handlerAddMoney.Handle(
                        new AddMoneyToFinancialAccountCommand
                        {
                            Balance = 15,
                            ClientId = client.Id
                        },
                        CancellationToken.None);

                        await handlerDeductMoney.Handle(
                        new DeductMoneyToFinancialAccountCommand
                        {
                            Balance = 7,
                            ClientId = client.Id
                        },
                        CancellationToken.None);
                    }

                    
                    // Логика начисления/списания для зарегистрированных пользователей
                    // Используйте registeredUsers[userIndex] для доступа к соответствующему пользователю
                    // Выполните начисление/списание для пользователя в этом потоке
                });

                tasks.Add(task);
            }
            // Дождитесь завершения всех задач
            await Task.WhenAll(tasks);

            // Проверка балансов всех пользователей
            foreach (var clientId in registeredUsers)
            {
                Assert.NotNull(
                await Context.FinancialAccounts.FirstOrDefaultAsync(financialAccounts =>
                    financialAccounts.ClientId == clientId && financialAccounts.Balance == 3));
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
