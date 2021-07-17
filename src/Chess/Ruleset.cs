using System;

namespace Chess
{
    public sealed class Ruleset
    {
        public static bool IsLegalMove(Piece piece, Vec2 move)
        {
            if (move.TaxicabLength() == 0)
            {
                return false;
            }
            // todo: check for things like whose turn it is
            switch (piece.Type)
            {
                case PieceType.Pawn:
                    int direction = (piece.Color == PieceColor.White) ? 1 : -1;
                    if (move.TaxicabLength() > 2)
                    {
                        return false;
                    }
                    // pawns may only move diagonally if the move will capture another piece of different affiliation
                    Tile nextTile = piece.Board[piece.Position + move];
                    bool canCapture = nextTile.Piece != null;
                    if (canCapture)
                    {
                        canCapture = nextTile.Piece.Color != piece.Color;
                    }
                    if (move.X > 0 && !canCapture)
                    {
                        return false;
                    }
                    if (Math.Abs(move.X) > 1)
                    {
                        return false;
                    }
                    if (move.Y < direction)
                    {
                        return false;
                    }
                    return true;
                // todo: implement more piece cases
            }
            throw new Exception("This piece type is not implemented!");
        }
        // this class should not be instantiated
        private Ruleset() { }
    }
}
