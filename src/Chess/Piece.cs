namespace Chess
{
    public enum PieceColor
    {
        White,
        Black
    }
    public enum PieceType
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King
    }
    public class Piece
    {
        public Piece(Vec2 position, PieceColor color)
        {
            Position = position;
            Color = color;
        }
        public Vec2 Position { get; private set; }
        public PieceColor Color { get; private set; }
    }
}
