using Minesweeper.Models;
using Minesweeper.Logic.Random;

namespace Minesweeper.Logic
{
    /// <summary>
    /// The BoardLogic class is responsible for all gameplay logic.
    /// This includes placing bombs, counting neighboring bombs,
    /// revealing cells, and determining win conditions.
    /// </summary>
    public class BoardLogic
    {
        // Random provider dependency (used for bomb placement)
        private readonly IRandomProvider _random;

        /// <summary>
        /// Constructor receives a random provider dependency.
        /// This allows bomb placement to be controlled and testable.
        /// </summary>
        public BoardLogic(IRandomProvider randomProvider)
        {
            _random = randomProvider;
        }

        /// <summary>
        /// Places bombs randomly on the board based on Difficulty value.
        /// Ensures bombs are not placed twice in the same location.
        /// </summary>
        public void SetupBombs(Board board)
        {
            int size = board.Size;

            // Total bombs to place is determined by board difficulty
            int bombsToPlace = board.Difficulty;
            int placed = 0;

            // Continue placing bombs until desired count is reached
            while (placed < bombsToPlace)
            {
                int r = _random.Next(0, size);
                int c = _random.Next(0, size);

                // Only place bomb if that cell does not already contain one
                if (!board.Cells[r, c].IsBomb)
                {
                    board.Cells[r, c].IsBomb = true;
                    placed++;
                }
            }
        }

        /// <summary>
        /// Calculates number of bombs surrounding each cell.
        /// Bomb cells are marked with 9 for display consistency.
        /// </summary>
        public void CountBombsNearby(Board board)
        {
            int size = board.Size;

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    Cell cell = board.Cells[r, c];

                    // If cell itself is a bomb, assign special value 9
                    if (cell.IsBomb)
                    {
                        cell.NumberOfBombNeighbors = 9;
                        continue;
                    }

                    int count = 0;

                    // Check all 8 surrounding positions
                    for (int rr = r - 1; rr <= r + 1; rr++)
                    {
                        for (int cc = c - 1; cc <= c + 1; cc++)
                        {
                            // Skip the cell itself
                            if (rr == r && cc == c)
                                continue;

                            // Ensure indices are within board boundaries
                            if (rr >= 0 && cc >= 0 && rr < size && cc < size)
                            {
                                if (board.Cells[rr, cc].IsBomb)
                                    count++;
                            }
                        }
                    }

                    cell.NumberOfBombNeighbors = count;
                }
            }
        }

        /// <summary>
        /// Reveals a specific cell selected by the user.
        /// If the cell contains 0 neighboring bombs,
        /// automatically reveals surrounding cells (recursive behavior).
        /// </summary>
        public void RevealCell(Board board, int row, int col)
        {
            // Prevent invalid index access
            if (row < 0 || col < 0 || row >= board.Size || col >= board.Size)
                return;

            Cell cell = board.Cells[row, col];

            // Do not reveal already revealed cells
            if (cell.IsVisited)
                return;

            // Mark cell as revealed
            cell.IsVisited = true;

            // If the cell is empty and has no neighboring bombs,
            // reveal adjacent cells recursively
            if (!cell.IsBomb && cell.NumberOfBombNeighbors == 0)
            {
                for (int r = row - 1; r <= row + 1; r++)
                {
                    for (int c = col - 1; c <= col + 1; c++)
                    {
                        if (r == row && c == col)
                            continue;

                        RevealCell(board, r, c);
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether the player has won.
        /// Player wins when every non-bomb cell has been revealed.
        /// </summary>
        public bool CheckWin(Board board)
        {
            for (int r = 0; r < board.Size; r++)
            {
                for (int c = 0; c < board.Size; c++)
                {
                    Cell cell = board.Cells[r, c];

                    // If any safe cell is not revealed, game continues
                    if (!cell.IsBomb && !cell.IsVisited)
                        return false;
                }
            }

            // All safe cells revealed
            return true;
        }
    }
}

