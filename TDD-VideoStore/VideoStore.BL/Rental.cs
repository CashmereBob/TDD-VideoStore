using System;

namespace VideoStore.BL
{
    public class Rental
    {
        public string movieTitle;
        public string socialSecurityNumber;
        public DateTime dueDate;

        public Rental(string movieTitle, string socialSecurityNumber, DateTime dateTime) 
        {
            this.dueDate = dateTime;
            this.movieTitle = movieTitle;
            this.socialSecurityNumber = socialSecurityNumber;
        }


    }
}