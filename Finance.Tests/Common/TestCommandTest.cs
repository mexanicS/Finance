using Finance.Persistence;
using Finance.WebApi;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly FinanceDbContext Context;
        protected readonly IMediator Mediator;
        protected readonly FinanceContextFactory Fact;
        public TestCommandBase()
        {
            var contextFactory = new FinanceContextFactory();
            Fact = contextFactory;
            Context = contextFactory.CreateContext();
            Mediator = Substitute.For<IMediator>();
        }

        public void Dispose()
        {
            FinanceContextFactory.Destroy(Context);
        }
    }
}
