using Finance.Application.Clinents.Commands.CreateClient;
using Finance.Application.Interfaces;
using Finance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Clinents.Commands.UpdateFinancialAccount
{
    public class UpdateFinancialAccountCommandHandler
            : IRequestHandler<UpdateFinancialAccountCommand, Unit>
    {
        private readonly IFinanceDbContext _dbContext;

        public UpdateFinancialAccountCommandHandler(IFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.FinancialAccounts.FirstOrDefaultAsync(client => client.Id == request.Id, cancellationToken);

            if (entity == null) { }

            return Unit.Value;
        }
    }
}
