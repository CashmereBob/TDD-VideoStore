using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.BL
{
    public class Rentals : IRentals
    {
        private IDateTime dateTime;
        private List<Rental> rentals;

        public Rentals(IDateTime dateTime)
        {
            this.dateTime = dateTime;
            rentals = new List<Rental>();
        }

        public void AddRental(string movieTitle, string socialSecurityNumber)
        {
            var customerRentals = GetRentalsFor(socialSecurityNumber);
            if (customerRentals.Count() != 0)
            {
                var rentalsWithDueDate = new List<Rental>();

                rentalsWithDueDate = customerRentals.Where(r => r.dueDate < dateTime.Now()).ToList();

                if (rentalsWithDueDate.Count() > 0)
                {
                    throw new RentalAllocationException("Late returns: ", rentalsWithDueDate);
                }
            }

            if (customerRentals.Any(r => r.movieTitle == movieTitle))
            {
                throw new RentalAllocationException("Already rented a copy of that movie");
            }


            if (customerRentals.Count() >= 3)
            {
                throw new RentalAllocationException("Exceeded maximum allowed rentals at the same time.");

            }

            var dueDate = dateTime.Now();

            rentals.Add(new Rental(movieTitle, socialSecurityNumber, dueDate.AddDays(3)));

        }

        public List<Rental> GetRentalsFor(string socialSecurityNumber)
        {
            return rentals.Where(x => x.socialSecurityNumber == socialSecurityNumber).ToList();
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
        }
    }
}
