using Finance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Persistence.EntityTypeConfiguration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasIndex(finance=>finance.Id).IsUnique();

            builder.Property(finance => finance.FirstName).HasMaxLength(258);

            builder.Property(finance => finance.LastName).HasMaxLength(258);

            builder.Property(finance => finance.MiddleName).HasMaxLength(258);

            builder.Property(finance => finance.Description).HasMaxLength(1000);
        }
    }
}
