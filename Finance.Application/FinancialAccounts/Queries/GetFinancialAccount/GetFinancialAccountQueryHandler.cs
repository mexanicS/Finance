using Finance.Application.Common.Exceptions;
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
    public class GetFinancialAccountQueryHandler :IRequestHandler<GetFinancialAccountQuery, FinancialAccount>
    {
        private readonly IFinanceDbContext _dbContext;

        public GetFinancialAccountQueryHandler(IFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FinancialAccount> Handle (GetFinancialAccountQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.FinancialAccounts.FirstOrDefaultAsync(financialAccounts => financialAccounts.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(FinancialAccount), request.Id);
            }

            return entity;
        }

    }
}
