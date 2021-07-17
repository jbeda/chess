using Xunit;

namespace Chess.Tests
{
    public class Piece_Parse
    {
        [Theory]
        [InlineData('p', PieceColor.Black, PieceType.Pawn)]
        [InlineData('n', PieceColor.Black, PieceType.Knight)]
        [InlineData('b', PieceColor.Black, PieceType.Bishop)]
        [InlineData('q', PieceColor.Black, PieceType.Queen)]
        [InlineData('k', PieceColor.Black, PieceType.King)]
        [InlineData('K', PieceColor.White, PieceType.King)]
        public void Parse(char c, PieceColor colorExpected, PieceType typeExpected)
        {
            var p = Piece.FromChar(c);
            Assert.Equal(colorExpected, p.Color);
            Assert.Equal(typeExpected, p.Type);
            
        }
    }
}