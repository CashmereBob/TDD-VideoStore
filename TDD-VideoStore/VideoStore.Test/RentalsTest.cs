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
            testMovie.Title = "Steven Segal - Hard Combat 8";
            sut.AddRental(testMovie.Title, testCustomer.SSN);

            var rentalsOnCustomer = sut.GetRentalsFor(testCustomer.SSN);

            Assert.AreEqual(2, rentalsOnCustomer.Count);
            

        }
        [Test]
        public void Cannot_Rent_More_Than_Three_Movies()
        {
            sut.AddRental(testMovie.Title, testCustomer.SSN);
            testMovie.Title = "Steven Segal - Hard Combat 8";
            sut.AddRental(testMovie.Title, testCustomer.SSN);
            testMovie.Title = "Dunder Honung";
            sut.AddRental(testMovie.Title, testCustomer.SSN);
            Assert.Throws<RentalAllocationException>(() =>
            {
                testMovie.Title = "Benny går i skolan";
                sut.AddRental(testMovie.Title, testCustomer.SSN);
            });
            Assert.That(sut.GetRentalsFor(testCustomer.SSN).Count == 3);
        }
        [Test]
        public void Customer_Cannot_Rent_Two_Copies_Of_The_Same_Movie()
        {
            sut.AddRental(testMovie.Title, testCustomer.SSN);
            Assert.Throws<RentalAllocationException>(() =>
            {
                sut.AddRental(testMovie.Title, testCustomer.SSN);
            });

        }
        [Test]
        public void Can_Not_Rent_If_Rental_With_Late_DueDate_Exists()
        {

            var fakeDate = new DateTime(2017, 05, 12);

            dateTime.Now().Returns(fakeDate);

            sut.AddRental(testMovie.Title, testCustomer.SSN);

            dateTime.Now().Returns(fakeDate.AddDays(5));


            var e = Assert.Throws<RentalAllocationException>(() =>
            {
                sut.AddRental("Nils Holgersson", testCustomer.SSN);
            });
            Assert.That(e.DueRentals.FirstOrDefault().movieTitle == testMovie.Title);
            Assert.IsTrue(e.Message.Contains(testMovie.Title));
            Assert.IsTrue(e.Message.Contains(fakeDate.AddDays(3).ToShortDateString()));





        }


    }
}
