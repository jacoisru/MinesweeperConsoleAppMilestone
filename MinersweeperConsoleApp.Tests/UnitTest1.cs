using Xunit;
using Minesweeper.Models;
using Minesweeper.Logic;
using Minesweeper.Logic.Random;

namespace MinesweeperConsoleApp.Tests
{
    public class BoardLogicTests
    {
        [Fact]
        public void SetupBombs_DoesNotThrowException()
        {
            // Arrange
            Board board = new Board(5);
            IRandomProvider random = new DefaultRandomProvider();
            BoardLogic logic = new BoardLogic(random);

            // Act
            Exception ex = Record.Exception(() =>
            {
                logic.SetupBombs(board);
            });

            // Assert
            Assert.Null(ex);
        }

        [Fact]
        public void CountBombsNearby_BombCell_IsSetToNine()
        {
            // Arrange
            Board board = new Board(3);
            IRandomProvider random = new DefaultRandomProvider();
            BoardLogic logic = new BoardLogic(random);

            // Force a bomb at (1,1)
            board.Cells[1, 1].IsBomb = true;

            // Act
            logic.CountBombsNearby(board);

            // Assert
            Assert.Equal(9, board.Cells[1, 1].NumberOfBombNeighbors);
        }
    }
}
