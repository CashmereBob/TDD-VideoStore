using System;

namespace VideoStore.BL
{
    public class Movie
    {
        private string title;

        public string Title
        {
            get { return title; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new FormatException("Title cannot be null or whitespace");
                }
                title = value;
            }
        }


        public Movie()
        {

        }
        public Movie(string title)
        {
            Title = title;
        }
    }
}