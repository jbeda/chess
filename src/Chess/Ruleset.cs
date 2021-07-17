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
                    {
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
                    }
                    // if the previous check passed, then move.Y * 2 must also equal the taxicab length
                    return true;
                case PieceType.Rook:
                    // a rook may only move in one cardinal direction
                    return !((Math.Abs(move.X) > 0) && (Math.Abs(move.Y) > 0));
                case PieceType.Queen:
                    // the queen can move in one direction per move, but as far as she wants
                    {
                        int directionCount = 0;
                        {
                            int length = move.TaxicabLength();
                            if ((length % 2 == 0) && (move.X * 2 == length))
                            {
                                directionCount++;
                            }
                        }
                        if ((Math.Abs(move.X) > 0) && (move.Y == 0))
                        {
                            directionCount++;
                        }
                        else if ((move.X == 0) && (Math.Abs(move.Y) > 0))
                        {
                            directionCount++;
                        }
                        return directionCount == 1;
                    }
                case PieceType.King:
                    // the king, much like the queen, can move in one direction and one direction only, however, he can only move one space at a time
                    {
                        int directionCount = 0;
                        {
                            int length = move.TaxicabLength();
                            if ((length % 2 == 0) && (move.X * 2 == length) && (length == 2))
                            {
                                directionCount++;
                            }
                        }
                        if ((Math.Abs(move.X) > 0) && (move.Y == 0) && (move.TaxicabLength() == 1))
                        {
                            directionCount++;
                        }
                        else if ((move.X == 0) && (Math.Abs(move.Y) > 0) && (move.TaxicabLength() == 1))
                        {
                            directionCount++;
                        }
                        return directionCount == 1;
                    }
            }
            throw new Exception("This piece type is not implemented!");
        }
        // this class should not be instantiated
        private Ruleset() { }
    }
}
