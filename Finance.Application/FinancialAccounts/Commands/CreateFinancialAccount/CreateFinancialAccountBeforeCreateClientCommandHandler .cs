using Finance.Application.Interfaces;
using Finance.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.FinancialAccounts.Commands.CreateFinancialAccount
{
    public class CreateFinancialAccountBeforeCreateClientCommandHandler : IRequestHandler<CreateFinancialAccountCommand, Guid>
    {
        private readonly IFinanceDbContext _dbContext;

        public CreateFinancialAccountBeforeCreateClientCommandHandler(IFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            var financialAccount = new FinancialAccount()
            {
                Id = Guid.NewGuid(),
                ClientId = request.ClientId,
                CreateDate = DateTime.UtcNow,
                Title = "Создано автоматически при создании клиента"
            };

            await _dbContext.FinancialAccounts.AddAsync(financialAccount, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return financialAccount.Id;
        }
    }
}
