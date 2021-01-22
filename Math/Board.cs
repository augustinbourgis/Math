using System;
using System.Collections.Generic;
using System.Text;

namespace Math
{
    class Board
    {
        public List<List<Cell>> _cells;
        public int _size;
        public List<Path> _paths;
        public Path _path;

        public static void Main(string[] args)
        {

            Board board = new Board(6);
            board.computeLinkedCells();
            board.solve();
            Console.WriteLine(board._path.toString());
            Console.ReadLine();
        }

        private Cell getCell(int i, int j)
        {
            return _cells[i][j];
        }

        public Board(int size)
        {
            _size = size;
            _cells = new List<List<Cell>>();
            _paths = new List<Path>();
            for(int i = 0; i < _size; i++)
            {
                List<Cell> lineCells = new List<Cell>();
                for(int j = 0; j < _size; j++)
                {
                    Cell cell = new Cell(i, j);
                    lineCells.Add(cell);
                }
                _cells.Add(lineCells);
            }
            computeLinkedCells();
        }

        private void computeLinkedCells()
        {
            foreach(List<Cell> cs in _cells)
            {
                foreach(Cell c in cs)
                {
                    c._possibleCells = GetPossibleCells(c);
                }
            }
        }

        private List<Cell> GetPossibleCells(Cell c)
        {
            int[] xMove = { 2, 1, -1, -2, -2, -1, 1, 2 };
            int[] yMove = { 1, 2, 2, 1, -1, -2, -2, -1 };
            List<Cell> res = new List<Cell>();
            for (int i = 0; i < 8; i++)
            {
                if(c._x+xMove[i]>=0 && c._x+xMove[i] < this._size && c._y+yMove[i]>=0 && c._y+yMove[i] < this._size)
                {
                    res.Add(getCell(c._x+xMove[i], c._y+yMove[i]));
                }
            }
            return res;
        }

        private bool getNextCell(Path currentPath)
        {
            // Si on arrive à la fin du chemin
            if (currentPath._length >= this._size * this._size)
            {
                // Victoir !
                if (currentPath._newCell._possibleCells.Contains(currentPath._startCell))
                {
                    _path = currentPath;
                    return true;
                }
                // non
                else
                {
                    return false;
                }
            }

            List<Cell> possibleCellAll = currentPath._newCell._possibleCells;
            List<Cell> possibleCellOpti = new List<Cell>();

            foreach (Cell c in possibleCellAll)
            {
                if (!currentPath._visited[c._x, c._y])
                {
                    // Les angles
                    if(c._possibleCells.Count == 2)
                    {
                        // 'c' -->  c'est l'angle
                        possibleCellOpti = new List<Cell>() { c };
                        break;
                    }
                    possibleCellOpti.Add(c);
                }
            }

            foreach (Cell c in possibleCellOpti)
            {
                if (getNextCell(new Path(this, currentPath, c))) return true;
            }

            return false;
        }

        private void solve()
        {
            Cell cellStart = getCell(0, 0);
            getNextCell(new Path(this, null, cellStart));
        }

        private Boolean areLinked(Cell cellFrom, Cell cellTo)
        {
            return cellFrom._possibleCells.Contains(cellTo);
        }

        private Boolean isVisited(Path path,Cell cell)
        {
            int x = cell._x;
            int y = cell._y;
            Boolean visited = path._visited[x,y];
            return visited;
        }
    }
}




/*
import numpy as np

def euler(chess, k, n, sol =[]):
    if len(sol) == 0:
        if n == 64:
            if k in bond[0]: sol.append(chess)
        else:

            poss =[m for m in bond[k] if chess[m] < 0]

            warn =[len([p for p in bond[m] if chess[p] < 0]) for m in poss]


               nchem_min = 0 if len(warn) == 0 else min(warn)
            candidat =[m for (i, m) in enumerate(poss) if warn[i] == nchem_min]

            for m in candidat:
                euler(chess[:m] +[n + 1] + chess[m + 1:], m, n + 1, sol)
    return sol

# Programme Principal ----------------------------------------------------------
rg,rg2 = range(8),range(64)

move =[(2, 1), (1, 2), (-1, 2), (-2, 1), (-2, -1), (-1, -2), (1, -2), (2, -1)]

bond =[[k + 8p + q for (p, q) in move if k//8+p in rg and k%8+q in rg] for k in rg2]

chess =[-1]64
chess[0] = 1

sol = euler(chess, 0, 1,[])

for _dep in range(64):
    print u'\nCase de dÃ©part %2d : (%d,%d)\n' % (_dep, _dep / 8, _dep % 8)
    chess =[1 + (k + (64 - sol[0][_dep])) % 64 for k in sol[0]]
    for k in np.reshape(chess, (8, 8)): print k
*/