using Xunit;

namespace Chess.Tests
{
    public class Board_LoadFromFENString
    {
        public Board_LoadFromFENString() {
            var game = new Game();
            mBoard = new Board(game);
        }

        [Fact]
        public void StartingPos()
        {
            mBoard.LoadFromFENString("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        }

        [Fact]
        public void FirstMove()
        {
            mBoard.LoadFromFENString("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1");
        }

        Board mBoard;
    }
}