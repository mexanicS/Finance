using MediatR;

namespace Finance.Application.FinancialAccountBalance.Commands.AddMoneyToFinancialAccount
{
    public class AddMoneyToFinancialAccountCommand : IRequest<Unit>
    {
        public Guid ClientId { get; set; }
        public decimal? Balance { get; set; }
    }
}
