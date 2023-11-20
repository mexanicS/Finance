using Finance.Application.Interfaces;
using Finance.Domain;
using MediatR;


namespace Finance.Application.FinancialAccounts.Commands.CreateFinancialAccount
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
                CreateDate = DateTime.UtcNow,
                Title = request.Title,
            };

            await _dbContext.FinancialAccounts.AddAsync(financialAccount, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return financialAccount.Id;
        }
    }
}
