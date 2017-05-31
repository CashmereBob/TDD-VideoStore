using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using VideoStore.BL;
using NSubstitute;

namespace VideoStore.Test
{
	[TestFixture]
	class RentalsTest
	{
		private IRentals sut;
        private IDateTime dateTime;
        private Customer testCustomer;
        private Movie testMovie;

        [SetUp]
        public void Setup()
        {
            dateTime = Substitute.For<IDateTime>();
            sut = new Rentals(dateTime);

            testCustomer = new Customer { Name = "Mange", SSN = "1910-05-23" };
            testMovie = new Movie { Title = "Rambo" };
        }

        [Test]
        public void Can_Add_Rental()
        {

            var fakeDate = new DateTime(2017, 05, 12);

            dateTime.Now().Returns(fakeDate);

            sut.AddRental(testMovie.Title, testCustomer.SSN);

            var rentalsOnCustomer = sut.GetRentalsFor(testCustomer.SSN);

            Assert.AreEqual(testMovie.Title, rentalsOnCustomer.FirstOrDefault().movieTitle);
            Assert.AreEqual(testCustomer.SSN, rentalsOnCustomer.FirstOrDefault().socialSecurityNumber);
            Assert.AreEqual(fakeDate.AddDays(3), rentalsOnCustomer.FirstOrDefault().dueDate);
        
        }

        [Test]
        public void Can_Get_Rental_By_SSN()
        {

            sut.AddRental(testMovie.Title, testCustomer.SSN);

            var rentalsOnCustomer = sut.GetRentalsFor(testCustomer.SSN);

            Assert.AreEqual(testMovie.Title, rentalsOnCustomer.FirstOrDefault().movieTitle);
            Assert.AreEqual(testCustomer.SSN, rentalsOnCustomer.FirstOrDefault().socialSecurityNumber);

        }
        [Test]
        public void Can_Rent_Multiple_Movies()
        {


            sut.AddRental(testMovie.Title, testCustomer.SSN);
            testMovie.Title = "Steven Segal - Har Combat 8";
            sut.AddRental(testMovie.Title, testCustomer.SSN);

            var rentalsOnCustomer = sut.GetRentalsFor(testCustomer.SSN);

            Assert.AreEqual(2, rentalsOnCustomer.Count);
            

        }


    }
}
