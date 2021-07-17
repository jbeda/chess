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
        public Piece(Vec2 position, PieceColor color, PieceType type, Board board)
        {
            Position = position;
            Color = color;
            Type = type;
            Board = board;
        }
        public bool Move(Vec2 moveDifference)
        {
            if (!Ruleset.IsLegalMove(this, moveDifference))
            {
                return false;
            }
            
            return true;
        }
        public Vec2 Position { get; private set; }
        public PieceColor Color { get; private set; }
        public PieceType Type { get; private set; }
        public Board Board { get; private set; }
    }
}
