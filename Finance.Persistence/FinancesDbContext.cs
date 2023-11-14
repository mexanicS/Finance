using Finance.Application.Interfaces;
using Finance.Domain;
using Finance.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Persistence
{
    public class FinancesDbContext : DbContext, IFinancialAccountDbContext
    {
        public DbSet<FinancialAccount> FinancialAccounts { get; set; }

        public FinancesDbContext(DbContextOptions<FinancesDbContext> options)
            : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FinanceConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
