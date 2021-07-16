using System;
using System.Collections.Generic;

namespace Chess
{
    // taken from wikipedia (https://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation)
    public sealed class FENString
    {
        private FENString(List<List<Piece>> pieces, PieceColor active, List<Piece> castlingAvailability, Vec2 enPassantTarget,
            int halfmoveClock, int fullmoveNumber)
        {
            mPieces = pieces;
            mActive = active;
            mCastlingAvailability = castlingAvailability;
            mEnPassantTarget = enPassantTarget;
            mHalfmoveClock = halfmoveClock;
            mFullmoveNumber = fullmoveNumber;
        }
        public static FENString Parse(string fenString)
        {
            // todo: parse
            throw new NotImplementedException();
        }
        public List<List<Piece>> Pieces => mPieces;
        public PieceColor Active => mActive;
        public List<Piece> CastlingAvailability => mCastlingAvailability;
        public Vec2 EnPassantTarget => mEnPassantTarget;
        public int HalfmoveClock => mHalfmoveClock;
        public int FullmoveNumber => mFullmoveNumber;
        private readonly List<List<Piece>> mPieces;
        private readonly PieceColor mActive;
        private readonly List<Piece> mCastlingAvailability;
        private readonly Vec2 mEnPassantTarget;
        private readonly int mHalfmoveClock;
        private readonly int mFullmoveNumber;
    }
    public struct Tile
    {
        public Tile(Vec2 position)
        {
            Color = ((position.X + position.Y) % 2 == 0) ? PieceColor.White : PieceColor.Black;
            Piece = null;
        }
        public Piece Piece { get; set; }
        public PieceColor Color { get; private set; }
    }
    public sealed class Board
    {
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
        public void Load(FENString fenString)
        {
            throw new NotImplementedException();
        }
        public int Width { get { return mWidth; } }
        public int Height { get { return mHeight; } }
        public Tile this[Vec2 position]
        {
            get
            {
                int index = Util.FlattenPosition(position, mWidth);
                return mTiles[index];
            }
            internal set
            {
                int index = Util.FlattenPosition(position, mWidth);
                mTiles[index] = value;
            }
        }
        public Game Game { get; private set; }
        private readonly int mWidth, mHeight;
        private readonly Tile[] mTiles;
    }
}
