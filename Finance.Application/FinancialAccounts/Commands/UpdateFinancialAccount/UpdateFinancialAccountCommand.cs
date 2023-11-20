using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.FinancialAccounts.Commands.UpdateFinancialAccount
{
    public class UpdateFinancialAccountCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public decimal? Balance { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string Title { get; set; }
    }
}
