using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VideoStore.BL
{
    public class RentalAllocationException : Exception
    {
        public List<Rental> DueRentals { get; set; }
        private string message;
        public override string Message
        {
            get
            {
                return message;
            }
        }
        public RentalAllocationException()
        {
        }

        public RentalAllocationException(string message)
        {
            this.message = message;
        }
        public RentalAllocationException(string message, List<Rental> dueRentals)
        {
            this.DueRentals = dueRentals;
            foreach (var lateReturn in dueRentals)
            {
               this.message = message += $@"\n Title: {lateReturn.movieTitle}, Due: {lateReturn.dueDate.ToShortDateString()}";
            }
        }

        public RentalAllocationException(string message, Exception innerException) : base(message,innerException)
        {
            this.message = message;
        }

        protected RentalAllocationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}