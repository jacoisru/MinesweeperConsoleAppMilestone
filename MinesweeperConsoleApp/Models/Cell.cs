using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models
{
    public class Cell
    {
        public int Row { get; set; } = -1;
        public int Column { get; set; } = -1;

        public bool IsVisited { get; set; } = false;
        public bool IsBomb { get; set; } = false;
        public bool IsFlagged { get; set; } = false;

        public int NumberOfBombNeighbors { get; set; } = 0;

        public bool HasSpecialReward { get; set; } = false;

        public Cell() { }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}

