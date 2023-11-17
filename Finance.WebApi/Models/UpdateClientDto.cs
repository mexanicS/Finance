using AutoMapper;
using Finance.Application.Clinents.Commands.CreateClient;
using Finance.Application.Clinents.Commands.UpdateClient;
using Finance.Application.Common.Mappings;
using Finance.Domain;

namespace Finance.WebApi.Models
{
    public class UpdateClientDto : IMapWith<UpdateClientCommand>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Description { get; set; }
        public DateTime DateOfBirth { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateClientDto, UpdateClientCommand>()
                .ForMember(clientDto => clientDto.Id, opt => opt.MapFrom(client => client.Id))
                .ForMember(clientDto => clientDto.FirstName, opt => opt.MapFrom(client => client.FirstName))
                .ForMember(clientDto => clientDto.LastName, opt => opt.MapFrom(client => client.LastName))
                .ForMember(clientDto => clientDto.MiddleName, opt => opt.MapFrom(client => client.MiddleName))
                .ForMember(clientDto => clientDto.DateOfBirth, opt => opt.MapFrom(client => client.DateOfBirth))
                .ForMember(clientDto => clientDto.Description, opt => opt.MapFrom(client => client.Description));
        }

    }
}
