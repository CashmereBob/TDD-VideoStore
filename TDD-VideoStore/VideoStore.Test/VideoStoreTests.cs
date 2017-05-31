using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStore.BL;
using NSubstitute;

namespace VideoStore.Test
{
    [TestFixture]
    public class VideoStoreTests
    {
        public IVideoStore sut { get; set; }
        public IRentals rentals { get; set; }

        public Customer testCustomer { get; set; }
        public Movie testMovie { get; set; }
        [SetUp]
        public void Setup()
        {
            rentals = Substitute.For<IRentals>();
            sut = new VideoStoreLibrary(rentals);

            testCustomer = new Customer { Name = "Mange", SSN="1910-05-23" };
            testMovie = new Movie { Title = "Rambo" };
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
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]

        public void Exception_Movie_Title_Cannot_Be_Null_Or_Empty(string input)
        {
            Assert.Throws<FormatException>(() =>
            {
                sut.AddMovie(new Movie(input));
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

        [Test]
        public void Cannot_Rent_Non_Existing_Movie()
        {
            sut.RegisterCustomer(testCustomer.Name, testCustomer.SSN);
            sut.RentMovie(testMovie.Title, testCustomer.SSN);

            rentals.DidNotReceive().AddRental(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Cannot_Rent_With_Non_Existing_Customer()
        {
            sut.AddMovie(testMovie);
            sut.RentMovie(testMovie.Title, testCustomer.SSN);

            rentals.DidNotReceive().AddRental(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Can_Rent_Movie()
        {
            sut.AddMovie(testMovie);
            sut.RegisterCustomer(testCustomer);

            sut.RentMovie(testMovie.Title, testCustomer.SSN);

            rentals.Received(1).AddRental(Arg.Is<string>(m => m.Contains(testMovie.Title)), Arg.Is<string>(m => m.Contains(testCustomer.SSN)));
        }

        [Test]
        public void Can_Return_Movie()
        {
            sut.AddMovie(testMovie);
            sut.RegisterCustomer(testCustomer);

            sut.RentMovie(testMovie.Title, testCustomer.SSN);
            sut.ReturnMovie(testMovie.Title, testCustomer.SSN);

            rentals.Received(1).RemoveRental(Arg.Is<string>(m => m.Contains(testMovie.Title)), Arg.Is<string>(m => m.Contains(testCustomer.SSN)));
        }





    }
}