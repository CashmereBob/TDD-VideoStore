using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.BL
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
