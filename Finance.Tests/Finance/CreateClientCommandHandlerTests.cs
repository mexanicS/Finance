using Finance.Application;
using Finance.Application.Clinents.Commands.CreateClient;
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
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Finance.Tests.Finance
{
    /*public class CreateClientCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateClientCommandHandler_Success()
        {
            
            // Arrange
            var handler = new CreateClientCommandsHandler(Context, Mediator);


            var firstName = "user";
            var lastName = "lasting";
            var middleName = "mid";
            var dateOfBirth = new DateTime(2006, 11, 29, 12, 0, 0);

            // Act
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

            // Assert
            Assert.NotNull(
                await Context.Clients.SingleOrDefaultAsync(client =>
                    client.Id == clientId &&
                    client.FirstName == firstName &&
                    client.LastName == lastName &&
                    client.MiddleName == middleName &&
                    client.DateOfBirth == dateOfBirth));
        }
    }*/
}
