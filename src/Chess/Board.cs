using System;
using System.Collections.Generic;

namespace Chess
{
    public class Tile
    {
        internal Tile(Vec2 position)
        {
            Color = ((position.X + position.Y) % 2 == 0) ? PieceColor.White : PieceColor.Black;
            Piece = null;
        }
        public Piece Piece { get; set; }
        public PieceColor Color { get; private set; }
    }
    public sealed class Board
    {
        // Bit Field to represent the set of castle moves still available
        [Flags]
        public enum CastleAvailableFlags {
            None = 0,
            WhiteQueen = 1,
            WhiteKing = 2,
            BlackQueen = 4,
            BlackKing = 8,
            All = WhiteQueen | WhiteKing | BlackQueen | BlackKing,
        }
        public Board(Game game, int width = 8, int height = 8)
        {
            game.Log.Print(string.Format("Creating board (width: {0}, height: {1})", width, height));
            Game = game;
            mWidth = width;
            mHeight = height;
            mTiles = new Tile[mWidth * mHeight];
            for (int x = 0; x < mWidth; x++)
            {
                for (int y = 0; y < mHeight; y++)
                {
                    var position = new Vec2(x, y);
                    this[position] = new Tile(position);
                }
            }
        }
        
        // Using definition of FENStrings from wikipedia
        // (https://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation)
public void LoadFromFENString(string fenString)
        {
            throw new NotImplementedException();
        }
        public int Width { get { return mWidth; } }
        public int Height { get { return mHeight; } }
        public Tile this[Vec2 position]
        {
            get
            {
                int index = Util.FlattenPosition(position, new Vec2(mWidth, mHeight));
                return mTiles[index];
            }
            internal set
            {
                int index = Util.FlattenPosition(position, new Vec2(mWidth, mHeight));
                mTiles[index] = value;
            }
        }
        public Game Game { get; private set; }


        private readonly int mWidth, mHeight;
        private readonly Tile[] mTiles;
        // Which color has the next move
        public PieceColor Active { get; set; } = PieceColor.White;
        public CastleAvailableFlags CastleAvailable { get; set; } = CastleAvailableFlags.All;
        // If a pawn has just made a two-square move, this is the position
        // "behind" the pawn. This is recorded regardless of whether there is a
        // pawn in position to make an en passant capture.
        public Vec2? EnPassantTarget { get; set; } = null;
        // The number of halfmoves since the last capture or pawn advance, used
        // for the fifty-move rule.
        public int HalfmoveClock { get; set; } = 0;
        // The number of the full move. It starts at 1, and is incremented after
        // Black's move.
        public int FullmoveNumber { get; set; } = 1;

    }
}
