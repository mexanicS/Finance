using Finance.Application.Interfaces;
using Finance.Domain;
using Finance.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Finance.Persistence
{
    public class FinanceDbContext : DbContext, IFinanceDbContext
    {
        public DbSet<FinancialAccount> FinancialAccounts { get; set; }
        public DbSet<Client> Clients { get; set; }

        public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
            : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FinancialAccountConfiguration());

            modelBuilder.ApplyConfiguration(new ClientConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(_connectionString);
            optionsBuilder.EnableSensitiveDataLogging();
            
            base.OnConfiguring(optionsBuilder);
        }
    }
}
