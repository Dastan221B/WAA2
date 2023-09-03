using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVC.Ground
{
    public class GroundModel
    {
        private Cell[,] _cells;

        public int Widht => _cells.GetUpperBound(0) + 1;
        public int Height => _cells.Length / Widht;

        public Cell[,] Cells => _cells;

        public void SetCells(Cell[,] cells)
        {
            _cells = cells;
        }

        public bool TryGetCellsByCoordinates(int x, int y, out Cell cell)
        {
            int rows = _cells.GetUpperBound(0) + 1;
            int columns = _cells.Length / rows;
            if (rows > x && columns > y)
            {
                cell = _cells[x, y];
                return true;
            }
            cell = null;
            return false;
        }

    }

}