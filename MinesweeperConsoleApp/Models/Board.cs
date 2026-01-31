using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models
{
    public class Board
    {
        public int Size { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public Cell[,] Cells { get; set; }

        // Difficulty = number of bombs
        public int Difficulty { get; set; } = 10;

        public int RewardsRemaining { get; set; } = 0;

        public Board(int size)
        {
            Size = size;
            StartTime = DateTime.Now;

            Cells = new Cell[size, size];
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    Cells[r, c] = new Cell(r, c);
                }
            }
        }
    }
}
