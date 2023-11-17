using AutoMapper;
using AutoMapper.QueryableExtensions;
using Finance.Application.Clinents.Commands.DeleteClient;
using Finance.Application.Common.Exceptions;
using Finance.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Clinents.Queries.GetClientList
{
    public class GetClientListQueryHandler : IRequestHandler<GetClientListQuery, ClientListVm>
    {
        private readonly IFinanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetClientListQueryHandler(IFinanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ClientListVm> Handle(GetClientListQuery request, CancellationToken cancellationToken)
        {
            var clientQuery = await _dbContext.Clients
                .Where(client=>client.Id == request.Id)
                .ProjectTo<ClientLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ClientListVm { Clients = clientQuery };
        }
    }
}
