using System.Collections.Concurrent;
using Finance.Application.Common.Exceptions;
using Finance.Application.Interfaces;
using Finance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Finance.Application.FinancialAccountBalance.Commands.AddMoneyToFinancialAccount
{

    public class AddMoneyToFinancialAccountCommandHandler : IRequestHandler<AddMoneyToFinancialAccountCommand, Unit>
    {
        private readonly IFinanceDbContext _dbContext;
        private static readonly ConcurrentDictionary<Guid, SemaphoreSlim> _semaphores = new();
        
        public AddMoneyToFinancialAccountCommandHandler(IFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddMoneyToFinancialAccountCommand request, CancellationToken cancellationToken)
        {
            var semaphore = _semaphores.GetOrAdd(request.ClientId, _ => new SemaphoreSlim(1, 1));
            await semaphore.WaitAsync(cancellationToken);
            
            try
            {
                var financialAccount =
                    await _dbContext.FinancialAccounts.FirstOrDefaultAsync(
                        client => client.ClientId == request.ClientId, cancellationToken);

                if (financialAccount == null)
                {
                    throw new NotFoundException(nameof(FinancialAccount), request.ClientId);
                }

                financialAccount.Balance += request.Balance;
                financialAccount.UpdateDate = DateTime.Now;

                await _dbContext.SaveChangesAsync(cancellationToken);
                Log.Information($"В потоке: {Environment.CurrentManagedThreadId}. На акк {request.ClientId} добавлено 10 единиц");
            }
            finally
            {
                semaphore.Release();
            }

            return Unit.Value;
        }
    }
    
}
