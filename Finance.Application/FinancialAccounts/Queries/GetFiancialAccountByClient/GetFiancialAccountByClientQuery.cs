using Finance.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.FinancialAccounts.Queries.GetFiancialAccountByClient
{
    public class GetFiancialAccountByClientQuery : IRequest<FinancialAccount>
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public decimal? Balance { get; set; } = null;

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string Title { get; set; }
    }
}