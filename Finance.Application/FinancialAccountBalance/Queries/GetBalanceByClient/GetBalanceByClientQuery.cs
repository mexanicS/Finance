using Finance.Domain;
using MediatR;

namespace Finance.Application.FinancialAccountBalance.Queries.GetBalanceByClient
{
    public class GetBalanceByClientQuery : IRequest<FinancialAccount>
    {
        public Guid ClientId { get; set; }

        public decimal? Balance { get; set; }

    }
}