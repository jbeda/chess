namespace Chess
{
    public enum PieceColor
    {
        White,
        Black
    }
    public class Piece
    {
        public Piece(Vec2 position, PieceColor color)
        {
            this.Position = position;
            this.Color = color;
        }
        public Vec2 Position { get; private set; }
        public PieceColor Color { get; private set; }
    }
}
