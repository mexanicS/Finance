using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Clinents.Queries.GetClientList
{
    public class ClientListVm
    {
        public IList<ClientLookupDto> Clients { get; set; }
    }
}
