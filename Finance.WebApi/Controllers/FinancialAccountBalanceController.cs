using Finance.Application.FinancialAccounts.Commands.CreateFinancialAccount;
using Finance.Application.FinancialAccounts.Commands.UpdateFinancialAccount;
using Finance.Application.FinancialAccounts.Commands.DeleteFinancialAccount;
using Finance.Application.FinancialAccounts.Queries.GetFinancialAccount;
using Finance.Domain;
using Finance.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Finance.Application.FinancialAccounts.Queries.GetFiancialAccountByClient;
using AutoMapper;
using Finance.Application.FinancialAccountBalance.Commands.AddMoneyToBalanceByClient;

namespace Finance.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class FinancialAccountBalanceController : BaseController
    {
        private readonly IMapper _mapper;
        public FinancialAccountBalanceController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("{clientId}")]
        public async Task<ActionResult<FinancialAccount>> GetBalanceByClient(Guid clientId)
        {
            var query = new GetBalanceByClientQuery()
            {
                ClientId = clientId,
            };

            var vm = await Mediator.Send(query);
            return Ok(vm.Balance);
        }

        [HttpPost("{clientId}")]
        public async Task<ActionResult<FinancialAccount>> AddMoneyToFinancialAccount(Guid clientId, decimal? amount)
        {
            var query = new AddMoneyToFinancialAccountCommand()
            {
                ClientId = clientId,
                Balance = amount,
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
