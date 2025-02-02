using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.FinancialAccountBalance.Commands.DeductMoneyToBalanceByClient
{
    public class DeductMoneyToFinancialAccountCommand : IRequest<Unit>
    {
        public Guid ClientId { get; set; }
        
        public decimal? Balance { get; set; }
    }
}
