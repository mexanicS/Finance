using Finance.Application.Common.Exceptions;
using Finance.Application.FinancialAccounts.Queries.GetFiancialAccountByClient;
using Finance.Application.FinancialAccounts.Queries.GetFinancialAccount;
using Finance.Application.Interfaces;
using Finance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.FinancialAccounts.Queries.GetFinancialAccount
{
    public class GetBalanceByClientQueryHandler : IRequestHandler<GetBalanceByClientQuery, FinancialAccount>
    {
        private readonly IFinanceDbContext _dbContext;

        public GetBalanceByClientQueryHandler(IFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FinancialAccount> Handle (GetBalanceByClientQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.FinancialAccounts.FirstOrDefaultAsync(financialAccounts => financialAccounts.ClientId == request.ClientId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(FinancialAccount), request.ClientId);
            }

            return entity;
        }

    }
}
