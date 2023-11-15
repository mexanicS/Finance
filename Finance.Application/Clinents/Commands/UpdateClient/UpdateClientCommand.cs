using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Clinents.Commands.UpdateClient
{
    public class UpdateClientCommand : IRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
