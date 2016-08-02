using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge1
{
    class Program
    {
        static void Main(string[] args)
        {
            int max = 100;
            int min = 1;

            Console.WriteLine("Pick a number between {0} and {1}", min, max);


            while (true)
            {
                int guess = (max + min) / 2;
                Console.WriteLine("Is your number {0} (higher/lower/equal)", guess);
                string input = Console.ReadLine();
                while(!input.Equals("higher")&&!input.Equals("lower") && !input.Equals("equal"))
                {
                    Console.WriteLine("Please write only \"higher\", \"lower\" or \"equal\" without the quotes.");
                    input = Console.ReadLine();
                }
                if (input.Equals("higher"))
                {
                    min = guess + 1;
                } else if (input.Equals("lower"))
                {
                    max = guess - 1;
                } else
                {
                    break;
                }
            }
            Console.WriteLine("Thank you for playing");
        }
    }
}
