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

        public string ClientId { get; set; }

        public decimal? Balance { get; set; } = null;

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set;}

        public string Title { get; set; }
    }
}
