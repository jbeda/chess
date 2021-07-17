using System;

namespace Chess
{
    public sealed class Ruleset
    {
        public static bool IsMoveLegal(Board board, Piece piece, Vec2 move)
        {
            Vec2? nullablePosition = null;
            for (int rank = 0; rank < board.Height; rank++)
            {
                for (int file = 0; file < board.Width; file++)
                {
                    var currentPosition = new Vec2(file, rank);
                    Tile tile = board[currentPosition];
                    if (tile.Piece == piece)
                    {
                        nullablePosition = currentPosition;
                    }
                }
            }
            if (nullablePosition == null)
            {
                throw new ArgumentException("The piece was not found on the board!");
            }
            var position = (Vec2)nullablePosition;
            if (Util.IsOutOfBounds(position + move, new Vec2(board.Width, board.Height)))
            {
                return false;
            }
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
                    Tile nextTile = board[position + move];
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
                case PieceType.Knight:
                    // a knight can only move in an L-like shape
                    if (move.TaxicabLength() != 3)
                    {
                        return false;
                    }
                    if (move.X == 3 || move.Y == 3)
                    {
                        return false;
                    }
                    return true;
                case PieceType.Bishop:
                    int length = move.TaxicabLength();
                    // the move must be diagonal, so the length must be an even number
                    if (length % 2 != 0)
                    {
                        return false;
                    }
                    if (move.X * 2 != length)
                    {
                        return false;
                    }
                    // if the previous check passed, then move.Y * 2 must also equal the taxicab length
                    return true;
                // todo: implement more piece cases
            }
            throw new Exception("This piece type is not implemented!");
        }
        // this class should not be instantiated
        private Ruleset() { }
    }
}
