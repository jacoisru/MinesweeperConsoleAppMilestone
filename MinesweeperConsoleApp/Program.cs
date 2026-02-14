using System;
using Minesweeper.Models;
using Minesweeper.Logic;
using Minesweeper.Logic.Random;

namespace MinesweeperConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // ============================================================
            // STEP 1: Ask the user for board size
            // ============================================================

            Console.WriteLine("Welcome to Minesweeper!");
            Console.Write("Enter board size (example: 8): ");

            string? sizeInput = Console.ReadLine();

            // TryParse prevents null warnings and prevents crashes
            if (!int.TryParse(sizeInput, out int size) || size <= 0)
            {
                Console.WriteLine("Invalid size entered. Using default size of 8.");
                size = 8;
            }

            // ============================================================
            // STEP 2: Create the board object
            // ============================================================

            // This initializes:
            // - Size
            // - StartTime
            // - 2D Cell array
            Board board = new Board(size);

            // ============================================================
            // STEP 3: Ask user for number of bombs (Difficulty)
            // ============================================================

            Console.Write("Enter number of bombs: ");
            string? bombInput = Console.ReadLine();

            if (!int.TryParse(bombInput, out int bombs) || bombs < 0)
            {
                Console.WriteLine("Invalid bomb count. Using default of 10.");
                bombs = 10;
            }

            // Assign difficulty
            board.Difficulty = bombs;

            // ============================================================
            // STEP 4: Create Logic Layer
            // ============================================================

            // Random provider (dependency injection)
            IRandomProvider randomProvider = new DefaultRandomProvider();

            // Business logic object
            BoardLogic logic = new BoardLogic(randomProvider);

            // ============================================================
            // STEP 5: Setup bombs and calculate neighbors
            // ============================================================

            logic.SetupBombs(board);
            logic.CountBombsNearby(board);

            // ============================================================
            // STEP 6: Print the board to console
            // ============================================================

            PrintBoard(board);

            Console.WriteLine("\nGame setup complete.");
            Console.ReadLine();
        }

        // ============================================================
        // PRINT BOARD METHOD
        // ============================================================
        // This method loops through the 2D cell array and prints:
        // - B for bombs
        // - Number for bomb neighbors
        // ============================================================

        static void PrintBoard(Board board)
        {
            Console.WriteLine("\nGenerated Board:\n");

            for (int r = 0; r < board.Size; r++)
            {
                for (int c = 0; c < board.Size; c++)
                {
                    Cell cell = board.Cells[r, c];

                    if (cell.IsBomb)
                    {
                        Console.Write(" B ");
                    }
                    else
                    {
                        Console.Write($" {cell.NumberOfBombNeighbors} ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}

