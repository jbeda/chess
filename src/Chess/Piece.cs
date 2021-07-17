using System;

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

        public static Piece FromChar(char c) {
            var l = Char.ToLower(c);
            const string PieceChars = "pnbrqk";
            var pos = PieceChars.IndexOf(l);
            if (pos < 0) {
                throw new ArgumentException();
            }

            return new Piece( l == c ? PieceColor.Black : PieceColor.White, (PieceType)pos);
        }

        public char ToChar() {
            var c = "PNBRQK"[(int)Type];
            if (Color == PieceColor.Black) {
                return Char.ToLower(c);
            }
            return c;
        }

        public PieceColor Color { get; private set; }
        public PieceType Type { get; private set; }
    }
}
