using AutoMapper;
using Finance.Application.Common.Mappings;
using Finance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Clinents.Queries.GetClientList
{
    public class ClientLookupDto : IMapWith<Client>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName => $"{FirstName} {LastName} {MiddleName}";

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, ClientLookupDto>()
                .ForMember(noteDto => noteDto.Id, opt => opt.MapFrom(client=>client.Id))
                .ForMember(noteDto => noteDto.FirstName, opt => opt.MapFrom(client => client.FirstName))
                .ForMember(noteDto => noteDto.LastName, opt => opt.MapFrom(client => client.LastName))
                .ForMember(noteDto => noteDto.MiddleName, opt => opt.MapFrom(client => client.MiddleName));
        }
    }
}
