using Finance.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.FinancialAccounts.Queries.GetFiancialAccountByClient
{
    public class GetBalanceByClientQuery : IRequest<FinancialAccount>
    {
        public Guid ClientId { get; set; }

        public decimal? Balance { get; set; }

    }
}