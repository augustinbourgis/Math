using System;
using System.Collections.Generic;
using System.Text;

namespace Math
{
    class Path
    {
        public Board _board;
        public Path _pathPred;
        public Cell _startCell;
        public Cell _newCell;
        public Boolean[,] _visited;
        public int _length;

        public Path()
        {

        }

        public Path(Board board, Path pathPred, Cell cell)
        {
            _board = board;
            _pathPred = pathPred;
            _newCell = cell;
            if (pathPred == null)
            {
                _length = 1;
            }
            else
            {
                _length = pathPred._length + 1;
            }
            int size = _board._size;
            _visited = new bool[size, size];
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if (pathPred == null)
                    {
                        _visited[i,j] = false;
                        _startCell = cell;
                    } else
                    {
                        _visited[i,j] = _pathPred._visited[i,j];
                        _startCell = _pathPred._startCell;
                    }
                }
            }
            int x = cell._x;
            int y = cell._y;
            _visited[x,y] = true;
        }

        public String toString()
        {
            String strPath = "";
            if(_pathPred != null)
            {
                strPath += _pathPred.toString() + " - ";
            }
            else
            {
                strPath += "Path = ";
            }
            strPath += _newCell.toString();
            return strPath;
        }
    }
}
