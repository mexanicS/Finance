using Finance.Application.Common.Exceptions;
using Finance.Application.FinancialAccountBalance.Commands.AddMoneyToBalanceByClient;
using Finance.Application.Interfaces;
using Finance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.FinancialAccountBalance.Commands.DeductMoneyToBalanceByClient
{
    public class DeductMoneyToFinancialAccountCommandHandler : IRequestHandler<DeductMoneyToFinancialAccountCommand, Unit>
    {
        private readonly IFinanceDbContext _dbContext;

        public DeductMoneyToFinancialAccountCommandHandler(IFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeductMoneyToFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            var financialAccount = await _dbContext.FinancialAccounts.FirstOrDefaultAsync(client => client.ClientId == request.ClientId, cancellationToken);

            if (financialAccount == null)
            {
                throw new NotFoundException(nameof(Client), request.ClientId);
            }

            if (request.Balance > financialAccount.Balance)
            {
                throw new InsufficientFundsException();
            }

            financialAccount.Balance -= request.Balance;
            financialAccount.UpdateDate = DateTime.Now;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
    
}
