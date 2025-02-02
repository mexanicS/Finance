using Finance.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Finance.Application.Interfaces
{
    public interface IFinanceDbContext
    {
        DbSet<FinancialAccount> FinancialAccounts { get; set; }

        DbSet<Client> Clients { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
        DatabaseFacade Database { get; }
    }
}
