using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Clinents.Queries.GetClientList
{
    public class GetClientListQuery : IRequest<ClientListVm>
    {
        public Guid Id { get; set; }
    }
}
