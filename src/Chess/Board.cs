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
            mBoardDims = (width, height);
            mTiles = new Tile[mBoardDims.X * mBoardDims.Y];
            for (int x = 0; x < mBoardDims.X; x++)
            {
                for (int y = 0; y < mBoardDims.Y; y++)
                {
                    var position = new Vec2(x, y);
                    this[position] = new Tile(position);
                }
            }
        }
        
        // Using definition of FENStrings from wikipedia
        // https://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation
        public void LoadFromFENString(string fenString)
        {
            var sections = fenString.Split(' ');
            if (sections.Length != 6) {
                throw new ArgumentException();
            }
            
            var posString = sections[0];
            var ranks = posString.Split('/');
            if (ranks.Length != 8) {
                throw new ArgumentException();
            }
            // FENString encoding starts at rank 8, we reverse to start at rank 1
            Array.Reverse(ranks);
            for (int rank = 0; rank < 8; rank++) {
                var rankString = ranks[rank];
                var file = 0;
                foreach (char pChar in rankString) {
                    if (file >= 8) {
                        // We have too many spots specified on this row/rank
                        throw new ArgumentException();
                    }
                    var index = Util.FlattenPosition((file, rank), BoardDims);                    
                    if (pChar >= '1' && pChar <= '8') {
                        var numBlank = pChar - '0';
                        while (numBlank > 0) {
                            mTiles[index].Piece = null;
                            numBlank--;
                            file++;
                            index = Util.FlattenPosition((file, rank), BoardDims);
                        }
                        continue;
                    }
                    var p = Piece.FromChar(pChar);
                    mTiles[index].Piece = p;
                    file++;
                }
            }

            var activeString = sections[1];
            Active = activeString switch
            {
                "w" => PieceColor.White,
                "b" => PieceColor.Black,
                _ => throw new ArgumentException()
            };

            var castlingString = sections[2];
            CastleAvailable = CastleAvailableFlags.None;
            if (castlingString != "-") {
                foreach (char c in castlingString) {
                    var castleBit = c switch
                    {
                        'K' => CastleAvailableFlags.WhiteKing,
                        'Q' => CastleAvailableFlags.WhiteQueen,
                        'k' => CastleAvailableFlags.BlackKing,
                        'q' => CastleAvailableFlags.BlackQueen,
                        _ => throw new ArgumentException(),
                    };
                    CastleAvailable |= castleBit;
                }
            }

            var enPassantString = sections[3];
            if (enPassantString == "-") {
                EnPassantTarget = null;
            } else {
                EnPassantTarget = Vec2.FromAlgebraic(enPassantString);
            }

            var halfmoveClockString = sections[4];
            int halfmove;
            if (!int.TryParse(halfmoveClockString, out halfmove)) {
                throw new ArgumentException();
            }
            HalfmoveClock = halfmove;

            var fullmovesString = sections[5];
            int fullmoves;
            if (!int.TryParse(fullmovesString, out fullmoves)) {
                throw new ArgumentException();
            }
        }
        public bool Move(Vec2 piecePosition, Vec2 newPosition)
        {
            var difference = newPosition - piecePosition;
            Piece piece = this[piecePosition].Piece;
            if (piece == null)
            {
                return false;
            }
            if (!Ruleset.IsMoveLegal(this, piece, difference))
            {
                return false;
            }
            // todo: check for en passant stuff,
            // capture a piece if theres any on the target square,
            // and cycle turns
            // however, we only need this implementation, for now
            this[piecePosition].Piece = null;
            this[piecePosition].Piece = piece;
            return true;
        }
        public int Width { get { return mBoardDims.X; } }
        public int Height { get { return mBoardDims.Y; } }
        public Vec2 BoardDims { get { return mBoardDims; } }
        public Tile this[Vec2 position]
        {
            get
            {
                int index = Util.FlattenPosition(position, BoardDims);
                return mTiles[index];
            }
            internal set
            {
                int index = Util.FlattenPosition(position, BoardDims);
                mTiles[index] = value;
            }
        }
        public Game Game { get; private set; }
        private readonly Vec2 mBoardDims;
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
