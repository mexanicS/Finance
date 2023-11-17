using Finance.Application.Clinents.Commands.CreateFinancialAccount;
using Finance.Application.Interfaces;
using Finance.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Finance.Application.Clinents.Commands.CreateFinancialAccount
{
    public class CreateFinancialAccountCommandsHandler
        : IRequestHandler<CreateFinancialAccountCommand,Guid>
    {
        private readonly IFinanceDbContext  _dbContext;

        public CreateFinancialAccountCommandsHandler(IFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            var financialAccount = new FinancialAccount()
            {
                Id = Guid.NewGuid(),
                Balance = request.Balance,
                ClientId = request.ClientId,
                CreateDate = DateTime.UtcNow,
                Title = request.Title,
                UpdateDate = null,
            };

            await _dbContext.FinancialAccounts.AddAsync(financialAccount, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return financialAccount.Id;

        }
    }
}
