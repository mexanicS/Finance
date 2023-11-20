using Finance.Application.Clinents.Commands.CreateClient;
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

namespace Finance.Application.Clinents.Commands.UpdateClient
{
    public class UpdateClientCommandHandler
            : IRequestHandler<UpdateClientCommand, Unit>
    {
        private readonly IFinanceDbContext _dbContext;

        public UpdateClientCommandHandler(IFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Clients.FirstOrDefaultAsync(client => client.Id == request.Id, cancellationToken);

            if (entity == null) 
            {
                throw new NotFoundException(nameof(Client), request.Id);
            }

            entity.Description = request.Description;
            entity.DateOfBirth = request.DateOfBirth;
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.MiddleName = request.MiddleName;
            entity.Description = request.Description;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
