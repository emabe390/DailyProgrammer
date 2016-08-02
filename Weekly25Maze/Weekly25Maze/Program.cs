using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly25Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game();


            g.RunGame();

            Console.WriteLine("Ending program...");
            Console.ReadLine();
        }
    }
}
