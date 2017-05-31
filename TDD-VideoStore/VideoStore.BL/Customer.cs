using System;

namespace VideoStore.BL
{
    public class Customer : IEquatable<Customer>
    {
        public string Name { get; set; }
        public string SSN { get; set; }

        public bool Equals(Customer other)
        {
            if(SSN == other.SSN)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}