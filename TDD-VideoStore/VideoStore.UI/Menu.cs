using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStore.BL;

namespace VideoStore.UI
{
    public class Menu
    {
        IVideoStore videoStore;
        IRentals rentals;
        IDateTime dateTime;
        public Menu()
        {
            dateTime = new DateTimeService();
            rentals = new Rentals(dateTime);
            videoStore = new VideoStoreLibrary(rentals);
        }
        private bool IsRunning { get; set; } = true;
        public void Run()
        {
            while (IsRunning)
            {
                Console.Clear();
                PrintOverallMenu();
                var userChoice = Console.ReadKey();
                var key = userChoice.Key;
                switch (key)
                {
                    // Rent Movie
                    case ConsoleKey.NumPad1:
                        RentMovie();
                        break;
                    case ConsoleKey.D1:
                        RentMovie();
                        break;
                    // Return Movie
                    case ConsoleKey.NumPad2:
                        ReturnMovie();
                        break;
                    case ConsoleKey.D2:
                        ReturnMovie();
                        break;

                    // Register Customer
                    case ConsoleKey.NumPad3:
                        RegisterUser();
                        break;
                    case ConsoleKey.D3:
                        RegisterUser();
                        break;

                    // Add Movie
                    case ConsoleKey.NumPad4:
                        AddMovie();
                        break;
                    case ConsoleKey.D4:
                        AddMovie();
                        break;
                    //Exit
                    case ConsoleKey.NumPad5:
                        Environment.Exit(-1);
                        break;
                    case ConsoleKey.D5:

                        Environment.Exit(-1);
                        break;
                    //Default
                    default:
                        Console.Clear();
                        break;
                }
            }
        }

        private void AddMovie()
        {
            Console.Clear();
            Console.WriteLine("Add movie");
            Console.WriteLine("----------");
            Console.Write("Title: ");

            string title = Console.ReadLine();

            try
            {
                videoStore.AddMovie(new Movie(title));
                Console.WriteLine($"Movie with {title} added.");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
            BackToMenu();
        }

        private void RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("Register customer");
            Console.WriteLine("-----------------");
            Console.Write("Name: ");

            string name = Console.ReadLine();

            Console.Write("SSN: ");

            string ssn = Console.ReadLine();

            try
            {
                videoStore.RegisterCustomer(new Customer { Name = name, SSN = ssn });
                Console.WriteLine($"{name} with SSN: {ssn} registered.");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
            BackToMenu();
        }

        private void ReturnMovie()
        {
            Console.Clear();
            Console.WriteLine("Return a movie");
            Console.WriteLine("------------");
            Console.Write("Customer SSN: ");

            string customer = Console.ReadLine();

            Console.Write("Movie title: ");

            string movie = Console.ReadLine();

            try
            {
                videoStore.ReturnMovie(movie, customer);
                Console.WriteLine($"{customer} returned {movie}.");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
            BackToMenu();
        }

        private void RentMovie()
        {
            Console.Clear();
            Console.WriteLine("Rent a movie");
            Console.WriteLine("------------");
            Console.Write("Customer SSN: ");

            string customer = Console.ReadLine();

            Console.Write("Movie title: ");

            string movie = Console.ReadLine();

            try
            {
                videoStore.RentMovie(movie, customer);
                Console.WriteLine($"{movie} added to {customer} rentals.");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
            BackToMenu();

        }

        private void PrintOverallMenu()
        {
            Console.WriteLine("Welcome to the VideoStore");
            Console.WriteLine("---------------------------");
            Console.WriteLine("1. Rent Movie");
            Console.WriteLine("2. Return Movie");
            Console.WriteLine("3. Register Customer");
            Console.WriteLine("4. Add Movie");
            Console.WriteLine("5. Exit");
        }

        private void BackToMenu()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Press any key to go back to menu.");
            Console.ReadKey();
        }
    }
}
