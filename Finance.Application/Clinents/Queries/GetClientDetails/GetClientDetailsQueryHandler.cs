using Finance.Application.Common.Exceptions;
using Finance.Application.Interfaces;
using Finance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Clinents.Queries.GetClientDetails
{
    public class GetFinancialAccountQueryHandler :IRequestHandler<GetClientDetailsQuery, Client>
    {
        private readonly IFinanceDbContext _dbContext;

        public GetFinancialAccountQueryHandler(IFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> Handle (GetClientDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Clients.FirstOrDefaultAsync(client => client.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Client), request.Id);
            }

            return entity;
        }

    }
}
