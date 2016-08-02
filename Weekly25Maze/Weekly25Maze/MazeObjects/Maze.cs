using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly25Maze.MazeObjects
{
    class Maze
    {
        public static Maze instance = new Maze();
        
        private Maze() { }

        Random random = new Random();

        int maxX;
        int maxY;

        MazeObject[][] actualMaze;

        internal void SetMaze(string[] stringMaze)
        {
            maxX = stringMaze[0].Length;
            maxY = stringMaze.Length;

            actualMaze = new MazeObject[maxY][];

            int currentY = 0;
            foreach(string row in stringMaze)
            {
                actualMaze[currentY] = new MazeObject[maxX];
                int currentX = 0;
                foreach (char square in row)
                {
                    actualMaze[currentY][currentX]= MazeObject.GetObjectByVisualApperance(square);

                    currentX++;
                }
                currentY++;
            }
        }

        private Space extraSpace;
        

        internal Player PlacePlayerRandomly()
        {
            Player p = new Player();

            int x;
            int y;
            do
            {
                x = random.Next(maxX);
                y = random.Next(maxY);
            } while (!actualMaze[y][x].VisualApperance().Equals(" "));

            p.x = x;
            p.y = y;

            extraSpace = (Space)actualMaze[y][x];

            actualMaze[y][x] = p;

            p.maze = this;

            return p;
        }

        internal void Tick()
        {
            Console.Clear();
            PrintGame();
        }


        public void PrintGame()
        {
            Console.WriteLine(Maze.instance.ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(MazeObject[] row in actualMaze)
            {
                foreach(MazeObject square in row)
                {
                    sb.Append(square.VisualApperance());
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }

        internal void MovePlayer(MazeObject mo, Player player)
        {
            actualMaze[player.y][player.x] = extraSpace;
            extraSpace.x = player.x;
            extraSpace.y = player.y;
            extraSpace = new Space();
            player.SetCoordinate(mo.GetCoordinate().Item1,mo.GetCoordinate().Item2);
            actualMaze[player.y][player.x] = player;
        }

        internal bool GetMazeObject(int x, int y, out MazeObject mazeobject)
        {
            try
            {
                mazeobject = actualMaze[y][x];
                mazeobject.SetCoordinate(x, y);
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                mazeobject = null;
                return false;
            }
        }
    }
}
