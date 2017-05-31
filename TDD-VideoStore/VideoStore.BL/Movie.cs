using System;

namespace VideoStore.BL
{
    public class Movie : IEquatable<Movie>
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

        public bool Equals(Movie other)
        {
            if(Title == other.Title)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}