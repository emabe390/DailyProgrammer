using System;

namespace Weekly25Maze.MazeObjects
{
    internal class Space : MazeObject
    {
        private bool isEnd = false;

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
            return (isEnd?"X":" ");
        }

        internal void SetEnd()
        {
            isEnd = true;
        }
    }
}