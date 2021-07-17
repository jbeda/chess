using Xunit;
using System;

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

            var piece = mBoard[(0, 0)].Piece;
            Assert.Equal(PieceColor.White, piece.Color);
            Assert.Equal(PieceType.Rook, piece.Type);

            piece = mBoard[(4, 7)].Piece;
            Assert.Equal(PieceColor.Black, piece.Color);
            Assert.Equal(PieceType.King, piece.Type);

            piece = mBoard[(2, 4)].Piece;
            Assert.Null(piece);

            Assert.Equal(PieceColor.White ,mBoard.Active);
            Assert.Equal(Board.CastleAvailableFlags.All, mBoard.CastleAvailable);
            Assert.Null(mBoard.EnPassantTarget);
            Assert.Equal(0, mBoard.HalfmoveClock);
            Assert.Equal(1, mBoard.FullmoveNumber);
        }

        [Fact]
        public void FirstMove()
        {
            mBoard.LoadFromFENString("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1");

            var piece = mBoard[(0, 0)].Piece;
            Assert.Equal(PieceColor.White, piece.Color);
            Assert.Equal(PieceType.Rook, piece.Type);

            piece = mBoard[(4, 7)].Piece;
            Assert.Equal(PieceColor.Black, piece.Color);
            Assert.Equal(PieceType.King, piece.Type);

            piece = mBoard[(4, 3)].Piece;
            Assert.Equal(PieceColor.White, piece.Color);
            Assert.Equal(PieceType.Pawn, piece.Type);

            piece = mBoard[(4, 1)].Piece;
            Assert.Null(piece);

            Assert.Equal(PieceColor.Black ,mBoard.Active);
            Assert.Equal(Board.CastleAvailableFlags.All, mBoard.CastleAvailable);
            Assert.Equal(mBoard.EnPassantTarget, new Vec2(4, 2));
            Assert.Equal(0, mBoard.HalfmoveClock);
            Assert.Equal(1, mBoard.FullmoveNumber);
        }

        [Fact]
        public void CastleAvailablePartial()
        {
            mBoard.LoadFromFENString("rnbqkbnr/pppppppp/11111111/8/4P3/8/PPPP1PPP/RNBQKBNR b kq e3 0 1");

            Assert.Equal(Board.CastleAvailableFlags.BlackKing | Board.CastleAvailableFlags.BlackQueen, mBoard.CastleAvailable);
        }

        [Fact]
        public void CastleAvailableNone()
        {
            mBoard.LoadFromFENString("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b - e3 0 1");

            Assert.Equal(Board.CastleAvailableFlags.None, mBoard.CastleAvailable);
        }

        [Theory]
        [InlineData("")]
        [InlineData("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1 foo")]
        [InlineData("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0")]
        [InlineData("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq z9 0 1")]
        [InlineData("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b foo e3 0 1")]
        [InlineData("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR z KQkq e3 0 1")]
        [InlineData("rnbqkbnr/pppppppp/88/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1")]
        [InlineData("rnbqkbnr/pppppppp/1/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1")]
        [InlineData("rnbqkbnr/ppppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1")]
        [InlineData("rnbqkbnr/pppppppp/9/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1")]
        [InlineData("rnbqkbnr/pppppppp/foo/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1")]
        [InlineData("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 foo 1")]
        [InlineData("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 -1 1")]
        [InlineData("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 bar")]
        public void BadStrings(string s)
        {
            Assert.Throws<ArgumentException>(() => mBoard.LoadFromFENString(s));
        }

        Board mBoard;
    }
}