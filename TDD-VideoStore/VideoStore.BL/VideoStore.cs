using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.BL
{
    class VideoStore : IVideoStore
    {
        private IRentals _rentals;
        public VideoStore(IRentals rentals)
        {
            this._rentals = rentals;
        }

        public void AddMovie(Movie movie)
        {
        }

        public List<Customer> GetCustomers()
        {
            return null;
        }

        public void RegisterCustomer(string name, string socialSecurityNumber)
        {
        }

        public void RentMovie(string movieTitle, string socialSecurityNumber)
        {
        }

        public void ReturnMovie(string movieTitle, string socialSecurityNumber)
        {
        }
    }
}
