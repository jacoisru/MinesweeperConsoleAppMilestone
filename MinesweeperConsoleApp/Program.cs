using Minesweeper.Logic;
using Minesweeper.Logic.Random;
using Minesweeper.Models;


Console.WriteLine("=== Minesweeper Console App (Milestone 1) ===");

// Board size
int size = 5;

// Create board
Board board = new Board(size);

// Create random provider
IRandomProvider randomProvider = new DefaultRandomProvider();

// Create logic
BoardLogic logic = new BoardLogic(randomProvider);

// Initialize board (THIS WAS THE MISSING PIECE)
logic.SetupBombs(board);

Console.WriteLine("Board setup completed successfully.");
Console.WriteLine("Milestone 1 requirements met.");

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
