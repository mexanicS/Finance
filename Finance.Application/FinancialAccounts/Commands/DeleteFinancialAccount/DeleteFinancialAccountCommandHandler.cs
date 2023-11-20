using Finance.Application.Common.Exceptions;
using Finance.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.FinancialAccounts.Commands.DeleteFinancialAccount
{
    public class DeleteFinancialAccountCommandHandler : IRequestHandler<DeleteFinancialAccountCommand, Unit>
    {
        private readonly IFinanceDbContext _dbContext;

        public DeleteFinancialAccountCommandHandler(IFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle (DeleteFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.FinancialAccounts.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Clinents), request.Id);
            }
            _dbContext.FinancialAccounts.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
