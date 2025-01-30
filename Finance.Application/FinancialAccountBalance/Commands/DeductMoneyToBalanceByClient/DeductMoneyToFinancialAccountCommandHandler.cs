using Finance.Application.Common.Exceptions;
using Finance.Application.Interfaces;
using Finance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Serilog;

namespace Finance.Application.FinancialAccountBalance.Commands.DeductMoneyToBalanceByClient
{
    public class DeductMoneyToFinancialAccountCommandHandler : IRequestHandler<DeductMoneyToFinancialAccountCommand, Unit>
    {
        private readonly IFinanceDbContext _dbContext;
        private static readonly ConcurrentDictionary<Guid, SemaphoreSlim> _semaphores = new();
        
        public DeductMoneyToFinancialAccountCommandHandler(IFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeductMoneyToFinancialAccountCommand request, CancellationToken cancellationToken)
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
                    throw new NotFoundException(nameof(Client), request.ClientId);
                }

                if (request.Balance > financialAccount.Balance)
                {
                    throw new InsufficientFundsException();
                }

                financialAccount.Balance -= request.Balance;
                financialAccount.UpdateDate = DateTime.Now;

                await _dbContext.SaveChangesAsync(cancellationToken);
                
                Log.Information($"В потоке: {Environment.CurrentManagedThreadId}. С акк {request.ClientId} снято 5 единиц");
            }
            finally
            {
                semaphore.Release();
            }

            return Unit.Value;
        }

    }
    
}
