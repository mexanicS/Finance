using AutoMapper;
using Finance.Application.Clinents.Commands.CreateClient;
using Finance.Application.Clinents.Commands.DeleteClient;
using Finance.Application.Clinents.Commands.UpdateClient;
using Finance.Application.Clinents.Queries.GetClientList;
using Finance.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finance.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : BaseController
    {
        private readonly IMapper _mapper;
        public ClientController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ClientListVm>> Get()
        {
            var query = new GetClientListQuery
            {
                UserId = UserId
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        //Получени инцы клиента по айди!

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateClientDto createClientDto) 
        {
            var command = _mapper.Map<CreateClientCommand>(createClientDto);
            command.UserId = UserId;
            var clientId = await Mediator.Send(command);
            return Ok(clientId);
        }

        [HttpPut]
        public async Task<ActionResult<Guid>> Update([FromBody] UpdateClientDto updateClientDto)
        {
            var command = _mapper.Map<UpdateClientCommand>(updateClientDto);
            command.UserId = UserId;
            var clientId = await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Guid>> Delete(Guid id)
        {
            var command = new DeleteClientCommand 
            { 
                UserId = UserId,
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }

    }
}
