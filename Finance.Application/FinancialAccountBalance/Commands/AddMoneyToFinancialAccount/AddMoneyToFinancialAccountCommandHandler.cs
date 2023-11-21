using Finance.Application.Common.Exceptions;
using Finance.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using Finance.Application.Interfaces;
using Finance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.FinancialAccountBalance.Commands.AddMoneyToBalanceByClient
{

    public class AddMoneyToFinancialAccountCommandHandler : IRequestHandler<AddMoneyToFinancialAccountCommand, Unit>
    {
        private readonly IFinanceDbContext _dbContext;

        public AddMoneyToFinancialAccountCommandHandler(IFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddMoneyToFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            var financialAccount = await _dbContext.FinancialAccounts.FirstOrDefaultAsync(client => client.ClientId == request.ClientId, cancellationToken);

            if (financialAccount == null)
            {
                throw new NotFoundException(nameof(FinancialAccount), request.ClientId);
            }
            
            financialAccount.Balance += request.Balance;
            financialAccount.UpdateDate = DateTime.Now;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
    
}
