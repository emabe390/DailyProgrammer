using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weekly25Maze.MazeObjects
{
    abstract class MazeObject
    {
        abstract public Tuple<int, int> GetCoordinate();
        abstract public string VisualApperance();
        public static MazeObject GetObjectByVisualApperance(char apperance)
        {
            switch (apperance)
            {
                case '#': return new Wall(); 
                case ' ': return new Space(); 
                case 'X': Space sp = new Space(); sp.SetEnd(); return sp; 
                default: return null;
            }
        }

        public abstract void SetCoordinate(int x, int y);

        public bool IsSquishable()
        {
            return (!VisualApperance().Equals("#") && !VisualApperance().Equals("X"));
        }

        internal void SetCoordinate(Tuple<int, int> tuple)
        {
            SetCoordinate(tuple.Item1, tuple.Item2);
        }
    }
}
