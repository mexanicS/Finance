using AutoMapper;
using Finance.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using Finance.Application.FinancialAccounts.Commands.UpdateFinancialAccount;
using Finance.Application.FinancialAccounts.Commands.DeleteFinancialAccount;
using Finance.Application.FinancialAccounts.Queries.GetFinancialAccount;
using Finance.Domain;
using Finance.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Finance.Application.FinancialAccounts.Queries.GetFiancialAccountByClient;

namespace Finance.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class FinancialAccountController : BaseController
    {
        private readonly IMapper _mapper;
        public FinancialAccountController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<FinancialAccount>> GetFinancialAccountByClient(Guid clientId)
        {
            var query = new GetFiancialAccountByClientQuery()
            {
                ClientId = clientId
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
