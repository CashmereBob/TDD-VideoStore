using System;

namespace VideoStore.BL
{
    public class MovieAllocationException: Exception
    {
        public MovieAllocationException()
        {

        }
        public MovieAllocationException(string message): base(message)
        {

        }
        public MovieAllocationException(string message, Exception e): base(message, e)
        {

        }
    }
}