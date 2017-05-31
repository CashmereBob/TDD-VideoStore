using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VideoStore.BL
{
    public class VideoStoreLibrary : IVideoStore
    {
        private IRentals _rentals;
        public List<Movie> Movies { get; set; }
        public List<Customer> Customers { get; set; }
        public VideoStoreLibrary(IRentals rentals)
        {
            this._rentals = rentals;
            Movies = new List<Movie>();
            Customers = new List<Customer>();
        }

        public void AddMovie(Movie movie)
        {
            if (Movies.Where(m => m.Title == movie.Title).Count() < 3)
            {
                Movies.Add(movie); 
            }
            else
            {
                throw new MovieAllocationException("Only 3 of the same movie allowed.");
            }
        }

        public List<Customer> GetCustomers()
        {
            return Customers;
        }

        public void RegisterCustomer(string name, string socialSecurityNumber)
        {
            if (!IsValidSSN(socialSecurityNumber))
            {
                throw new FormatException("Wrong format on entered SSN.");
            }
            
                if (Customers.Any(c => c.SSN == socialSecurityNumber))
                {
                    throw new CustomerAllocationException("A person with that SSN is already registered.");
                }
                else
                {
                    Customers.Add(new Customer() { Name = name, SSN = socialSecurityNumber });
                } 

        }

        private bool IsValidSSN(string ssn)
        {
            var pattern = @"^\d{4}-\d{2}-\d{2}$";
            if (Regex.IsMatch(ssn, pattern))
            {
                return true;
            }
            return false;
        }

        public void RentMovie(string movieTitle, string socialSecurityNumber)
        {
            if (Movies.Contains(new Movie(movieTitle)) && Customers.Contains(new Customer {SSN = socialSecurityNumber }))
            {

                _rentals.AddRental(movieTitle, socialSecurityNumber);

            }
        }

        public void ReturnMovie(string movieTitle, string socialSecurityNumber)
        {
            _rentals.RemoveRental(movieTitle, socialSecurityNumber);
        }


        public Movie GetMovie(string title)
        {
            return Movies.FirstOrDefault(m => m.Title == title);

        }

        public void RegisterCustomer(Customer customer)
        {
            Customers.Add(customer);
        }
    }
}
