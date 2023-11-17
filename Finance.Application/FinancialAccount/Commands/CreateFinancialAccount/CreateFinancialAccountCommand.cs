using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Finance.Application.Clinents.Commands.CreateFinancialAccount
{
    public class CreateFinancialAccountCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public string ClientId { get; set; }

        public decimal? Balance { get; set; } = null;

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string Title { get; set; }
    }
}
