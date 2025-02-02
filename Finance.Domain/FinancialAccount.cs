using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Domain
{
    public class FinancialAccount
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        //[ConcurrencyCheck]
        public decimal? Balance { get; set; } = 0;

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set;} = null;

        public string? Title { get; set; }
    }
}
