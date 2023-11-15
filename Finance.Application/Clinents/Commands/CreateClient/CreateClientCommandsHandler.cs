using Finance.Application.Interfaces;
using Finance.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Finance.Application.Clinents.Commands.CreateClient
{
    public class CreateClientCommandsHandler
        : IRequestHandler<CreateClientCommand,Guid>
    {
        private readonly IFinanceDbContext  _dbContext;

        public CreateClientCommandsHandler(IFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var client = new Client()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                DateOfBirth = request.DateOfBirth,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                AddedDate = DateTime.Now,
                Description = string.Empty,
            };

            await _dbContext.Clients.AddAsync(client, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return client.Id;

        }
    }
}
