using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.UI
{
    public class Menu
    {
        private bool IsRunning { get; set; } = true;
        public void Run()
        {
            while (IsRunning)
            {
                PrintOverallMenu();
                var userChoice = Console.ReadKey();
                var key = userChoice.Key;
                switch (key)
                {
                    // Rent Movie
                    case ConsoleKey.NumPad1:
                        break;

                    // Return Movie
                    case ConsoleKey.NumPad2:
                        break;

                    // Get Movies
                    case ConsoleKey.NumPad3:
                        break;

                    // Get Customers
                    case ConsoleKey.NumPad4:
                        break;
                    
                    // Register Customer
                    case ConsoleKey.NumPad5:
                        break;

                    // Add Movie
                    case ConsoleKey.NumPad6:
                        break;
                    case ConsoleKey.NumPad9:
                        Environment.Exit(-1);
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }

        private void PrintOverallMenu()
        {
            Console.WriteLine("Welcome to the VideoStore");
            Console.WriteLine("---------------------------");
            Console.WriteLine("1. Rent Movie");
            Console.WriteLine("2. Return Movie");
            Console.WriteLine("3. Get Movies");
            Console.WriteLine("4. Get Customers");
            Console.WriteLine("5. Register Customer");
            Console.WriteLine("6. Add Movie");
            Console.WriteLine("9. Exit");



        }
    }
}
