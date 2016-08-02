using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly25Maze.MazeObjects
{
    class Wall : MazeObject
    { 

        internal int x;
        internal int y;
        public override void SetCoordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override Tuple<int, int> GetCoordinate()
        {
            return new Tuple<int, int>(x, y);
        }

        public override string VisualApperance()
        {
            return "#";
        }


    }
}
