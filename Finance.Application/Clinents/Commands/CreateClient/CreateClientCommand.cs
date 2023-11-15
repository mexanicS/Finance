using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Finance.Application.Clinents.Commands.CreateClient
{
    public class CreateClientCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
