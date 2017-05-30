using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.BL
{
    public class CustomerAllocationException: Exception
    {
        public CustomerAllocationException()
        {
                
        }
        public CustomerAllocationException(string message): base(message)
        {

        }
        public CustomerAllocationException(string message, Exception e): base(message, e)
        {

        }
    }
}
