﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.BL
{
    public interface IVideoStore
    {
        void RegisterCustomer(string name, string socialSecurityNumber);
        void AddMovie(Movie movie);

        Movie GetMovie(string title);
        void RentMovie(string movieTitle, string socialSecurityNumber);
        List<Customer> GetCustomers();
        void ReturnMovie(string movieTitle, string socialSecurityNumber);
        void RegisterCustomer(Customer testCustomer);
    }
}
