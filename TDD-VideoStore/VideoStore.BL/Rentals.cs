using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.BL
{
    class Rentals : IRentals
    {
        public void AddRental(string movieTitle, string socialSecurityNumber)
        {
        }

        public List<Rental> GetRentalsFor(string socialSecurityNumber)
        {
            return null;
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
        }
    }
}
