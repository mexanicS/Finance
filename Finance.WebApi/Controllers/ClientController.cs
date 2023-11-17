using Finance.Application.Clinents.Queries.GetClientList;
using Microsoft.AspNetCore.Mvc;

namespace Finance.WebApi.Controllers
{
    public class ClientController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ClientListVm>> GetAllClients()
        {
            var query = new GetClientListQuery
            {
                UserId = UserId
            };

            var vm = await Mediator.Send(query);

            return Ok();
        }

        //[HttpGet]
        //public async Task<ActionResult<Client>> Get() {}

    }
}
