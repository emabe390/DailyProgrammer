using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly25Maze.MazeObjects;

namespace Weekly25Maze
{
    class Game
    {
        private static string[] maze1 = new string[] {
                                "#########################################################################",
                                "#   #               #               #           #                   #   #",
                                "#   #   #########   #   #####   #########   #####   #####   #####   #   #",
                                "#               #       #   #           #           #   #   #       #   #",
                                "#########   #   #########   #########   #####   #   #   #   #########   #",
                                "#       #   #               #           #   #   #   #   #           #   #",
                                "#   #   #############   #   #   #########   #####   #   #########   #   #",
                                "#   #               #   #   #       #           #           #       #   #",
                                "#   #############   #####   #####   #   #####   #########   #   #####   #",
                                "#           #       #   #       #   #       #           #   #           #",
                                "#   #####   #####   #   #####   #   #########   #   #   #   #############",
                                "#       #       #   #   #       #       #       #   #   #       #       #",
                                "#############   #   #   #   #########   #   #####   #   #####   #####   #",
                                "#           #   #           #       #   #       #   #       #           #",
                                "#   #####   #   #########   #####   #   #####   #####   #############   #",
                                "#   #       #           #           #       #   #   #               #   #",
                                "#   #   #########   #   #####   #########   #   #   #############   #   #",
                                "#   #           #   #   #   #   #           #               #   #       #",
                                "#   #########   #   #   #   #####   #########   #########   #   #########",
                                "#   #       #   #   #           #           #   #       #               #",
                                "#   #   #####   #####   #####   #########   #####   #   #########   #   #",
                                "#   #                   #           #               #               #   #",
                                "# X #####################################################################" };

        private static string[] maze2 = new string[] { "#####################################", "# #       #       #     #         # #", "# # ##### # ### ##### ### ### ### # #", "#       #   # #     #     # # #   # #", "##### # ##### ##### ### # # # ##### #", "#   # #       #     # # # # #     # #", "# # ####### # # ##### ### # ##### # #", "# #       # # #   #     #     #   # #", "# ####### ### ### # ### ##### # ### #", "#     #   # #   # #   #     # #     #", "# ### ### # ### # ##### # # # #######", "#   #   # # #   #   #   # # #   #   #", "####### # # # ##### # ### # ### ### #", "#     # #     #   # #   # #   #     #", "# ### # ##### ### # ### ### ####### #", "# #   #     #     #   # # #       # #", "# # ##### # ### ##### # # ####### # #", "# #     # # # # #     #       # #   #", "# ##### # # # ### ##### ##### # #####", "# #   # # #     #     # #   #       #", "# # ### ### ### ##### ### # ##### # #", "# #         #     #       #       # #", "#X###################################" };


        Player player;
        public bool hasDied = false;
        public bool hasWon = false;

        public Game()
        {
            
            Maze.instance.SetMaze(maze2);
            player = Maze.instance.PlacePlayerRandomly();
            player.g = this;
            Maze.instance.PrintGame();
        }

        public void RunGame()
        {
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();

                switch (keyinfo.Key)
                {
                    case (ConsoleKey.UpArrow): player.Move(Direction.UP); break;
                    case (ConsoleKey.DownArrow): player.Move(Direction.DOWN); break;
                    case (ConsoleKey.RightArrow): player.Move(Direction.RIGHT); break;
                    case (ConsoleKey.LeftArrow): player.Move(Direction.LEFT); break;
                    case (ConsoleKey.D): hasDied = true; break;
                    default: break;
                }
            }
            while (hasWon==false && hasDied == false);

            PrintEndScreen();
        }

        private void PrintEndScreen()
        {
            if (hasWon)
            {
                Console.Clear();
                Console.WriteLine("Congratulations! You won!");
            }
            else if (hasDied)
            {
                Console.Clear();
                Console.WriteLine("Congratulations! You died!");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Congratulations! Somehow you exited the program without either dying or winning. You are a true hacker");
            }
        }
    }
}
