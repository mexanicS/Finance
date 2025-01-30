using Finance.Application.Common.Exceptions;
using Finance.Application.Interfaces;
using Finance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Finance.Application.FinancialAccounts.Queries.GetFiancialAccountByClient
{
    public class GetFinancialAccountByClientQueryHandler : IRequestHandler<GetFiancialAccountByClientQuery, FinancialAccount>
    {
        private readonly IFinanceDbContext _dbContext;

        public GetFinancialAccountByClientQueryHandler(IFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FinancialAccount> Handle (GetFiancialAccountByClientQuery request, CancellationToken cancellationToken)
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
