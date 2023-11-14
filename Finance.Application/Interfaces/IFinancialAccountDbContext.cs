using Finance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Finance.Application.Interfaces
{
    public interface IFinancialAccountDbContext
    {
        DbSet<FinancialAccount> FinancialAccounts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
