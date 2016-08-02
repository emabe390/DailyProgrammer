using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly25Maze.MazeObjects
{
    public enum Direction { UP, RIGHT, LEFT, DOWN };
    

    class Player : MazeObject
    {
        internal Direction dir;
        internal int x;
        internal int y;
        internal Maze maze;
        internal Game g;

        public Player()
        {
            this.dir = Direction.UP;
        }

        
        
        public override Tuple<int, int> GetCoordinate()
        {
            return new Tuple<int, int>(x, y);
        }

        public override string VisualApperance()
        {
            switch (dir)
            {
                case Direction.UP: return "^";
                case Direction.DOWN: return "V";
                case Direction.RIGHT: return ">";
                case Direction.LEFT: return "<";
                default: return "false player direction";
            }
        }

        private Tuple<int,int> Movement(Direction d)
        {
            switch (d)
            {
                case (Direction.UP): return new Tuple<int, int>(0, -1);
                case (Direction.DOWN):return new Tuple<int, int>(0, 1);
                case (Direction.RIGHT):return new Tuple<int, int>(1, 0);
                case Direction.LEFT: return new Tuple<int, int>(-1, 0);

            }
            return null;
        }

        internal void Move(Direction d)
        {
            
            if (dir.Equals(d))
            {
                var m = Movement(d);
                TryMove(m.Item1, m.Item2);
            } else
            {
                dir = d;
            }


            maze.Tick();
            Console.WriteLine("I moved" + "dir: " + dir + "coordinates: " + x + " . " + y );
        }

        private void TryMove(int dx, int dy)
        {
            MazeObject mo;
            if(maze.GetMazeObject(dx+x, dy+y, out mo))
            {
                if(mo.VisualApperance().Equals(" "))
                {
                    maze.MovePlayer(mo, this);
                } else if (mo.VisualApperance().Equals("X"))
                {
                    g.hasWon = true;
                } else if (mo.VisualApperance().Equals("#"))
                {
                    MazeObject nextMo;
                    if(maze.GetMazeObject(dx+dx+x,dy+dy+y,out nextMo) && nextMo.IsSquishable())
                    {
                        maze.PushSquare(mo, nextMo, this);
                    }
                }
            }
        }

        public override void SetCoordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
