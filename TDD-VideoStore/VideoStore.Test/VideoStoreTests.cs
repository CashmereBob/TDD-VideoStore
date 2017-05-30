using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStore.BL;

namespace VideoStore.Test
{
    [TestFixture]
    public class VideoStoreTests
    {
        public IVideoStore sut { get; set; }
        public IRentals rentals { get; set; }
        [SetUp]
        public void Setup()
        {
            rentals = new Rentals();
            sut = new VideoStoreLibrary(rentals);
        }
        [Test]
        public void Can_Add_Movie()
        {
            sut.AddMovie(new Movie("Rambo"));

            var movie = sut.GetMovie("Rambo");

            Assert.That(movie.Title == "Rambo");


        }
        [Test]
        public void Cannot_Add_More_Than_Three_Of_The_Same_Movie()
        {
            sut.AddMovie(new Movie("Rambo"));
            sut.AddMovie(new Movie("Rambo"));
            sut.AddMovie(new Movie("Rambo"));
            Assert.Throws<MovieAllocationException>(() =>
           {
               sut.AddMovie(new Movie("Rambo"));
           });

        }
        [Test]
        public void Can_Register_Customer()
        {
            sut.RegisterCustomer("Mange", "1910-05-23");

            var customers = sut.GetCustomers();
            Assert.That(customers.Count == 1);
            Assert.That(customers.FirstOrDefault().SSN == "1910-05-23");
        }
        [Test]
        public void Cannot_Add_Same_Customer_Twice()
        {
            sut.RegisterCustomer("Mange", "1910-05-23");

            Assert.Throws<CustomerAllocationException>(() =>
            {
                sut.RegisterCustomer("Mange", "1910-05-23");

            });
        }

        [Test]
        public void Exception_On_Invalid_SSN_Format()
        {
            Assert.Throws<FormatException>(() =>
            {
                sut.RegisterCustomer("Robin", "10-10-100");
            });
        }
    }
}
