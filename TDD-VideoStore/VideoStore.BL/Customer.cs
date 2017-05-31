using System;

namespace VideoStore.BL
{
    public class Customer : IEquatable<Customer>
    {
        private string name { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new FormatException("Name cannot be null or whitespace");
                }
                name = value;
            }
        }

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