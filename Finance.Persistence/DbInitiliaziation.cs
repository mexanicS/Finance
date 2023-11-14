using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Persistence
{
    public class DbInitiliaziation
    {
        public static void Initilize(FinancesDbContext financesDbContext)
        {
            financesDbContext.Database.EnsureCreated();
        }
    }
}
