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
        public Piece(PieceColor color, PieceType type)
        {
            Color = color;
            Type = type;
        }
        public PieceColor Color { get; private set; }
        public PieceType Type { get; private set; }
    }
}
