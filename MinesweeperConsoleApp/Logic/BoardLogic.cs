using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.Logic.Random;
using Minesweeper.Models;

namespace Minesweeper.Logic
{
    public class BoardLogic
    {
        private readonly IRandomProvider _random;

        public BoardLogic(IRandomProvider randomProvider)
        {
            _random = randomProvider;
        }

        public void SetupBombs(Board board)
        {
            int size = board.Size;
            int totalCells = size * size;

            int bombsToPlace = board.Difficulty;
            if (bombsToPlace < 0) bombsToPlace = 0;
            if (bombsToPlace > totalCells) bombsToPlace = totalCells;

            // Reset bombs first
            for (int r = 0; r < size; r++)
                for (int c = 0; c < size; c++)
                    board.Cells[r, c].IsBomb = false;

            int placed = 0;
            while (placed < bombsToPlace)
            {
                int r = _random.Next(0, size);
                int c = _random.Next(0, size);

                if (!board.Cells[r, c].IsBomb)
                {
                    board.Cells[r, c].IsBomb = true;
                    placed++;
                }
            }
        }

        public void CountBombsNearby(Board board)
        {
            int size = board.Size;

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    Cell cell = board.Cells[r, c];

                    if (cell.IsBomb)
                    {
                        // Milestone requirement: bomb cell can be set to 9
                        cell.NumberOfBombNeighbors = 9;
                        continue;
                    }

                    int count = 0;

                    for (int rr = r - 1; rr <= r + 1; rr++)
                    {
                        for (int cc = c - 1; cc <= c + 1; cc++)
                        {
                            if (rr == r && cc == c) continue;
                            if (rr < 0 || cc < 0 || rr >= size || cc >= size) continue;

                            if (board.Cells[rr, cc].IsBomb)
                                count++;
                        }
                    }

                    cell.NumberOfBombNeighbors = count;
                }
            }
        }
    }
}
