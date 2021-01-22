using System;
using System.Collections.Generic;
using System.Text;

namespace Math
{
    class Cell
    {
        public int _x;
        public int _y;
        public List<Cell> _possibleCells;

        public Cell()
        {

        }

        public Cell(int x, int y)
        {
            _x = x;
            _y = y;
            _possibleCells = new List<Cell>();
        }

        public String toString()
        {
            string abcdef = "ABCDEFGH";
            return "[" + abcdef[_x] + "" + _y + "]";
        }

    }
}
