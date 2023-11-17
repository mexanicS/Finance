using Finance.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Clinents.Queries.GetClientDetails
{
    public class GetClientDetailsQuery : IRequest<Client>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Description { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
