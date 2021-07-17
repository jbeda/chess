using System;
using System.Collections.Generic;

namespace Chess.Frontends.Console
{
    internal sealed class RenderBuffer
    {
        public RenderBuffer()
        {
            mSize = new Vec2(0);
            mBuffer = new List<char>();
        }
        public void Clear()
        {
            for (int x = 0; x < mSize.X; x++)
            {
                for (int y = 0; y < mSize.Y; y++)
                {
                    mBuffer[Util.FlattenPosition(new Vec2(x, y), mSize.X)] = ' ';
                }
            }
        }
        public void Resize(Vec2 newSize)
        {
            Vec2 originalSize = mSize;
            mSize = newSize;
            var originalData = new List<char>();
            originalData.AddRange(mBuffer);
            mBuffer = new List<char>();
            for (int i = 0; i < mSize.X * mSize.Y; i++)
            {
                mBuffer.Add(' ');
            }
            int lesserWidth = (mSize.X > originalSize.X) ? originalSize.X : mSize.X;
            int lesserHeight = (mSize.Y > originalSize.Y) ? originalSize.Y : mSize.Y;
            for (int x = 0; x < mSize.X; x++)
            {
                for (int y = 0; y < mSize.Y; y++)
                {
                    int index = Util.FlattenPosition(new Vec2(x, y), mSize.X);
                    if (x < lesserWidth && y < lesserHeight)
                    {
                        mBuffer[index] = originalData[Util.FlattenPosition(new Vec2(x, y), originalSize.X)];
                    }
                }
            }
        }
        private void VerifyPosition(Vec2 position)
        {
            if (Util.IsOutOfRange(position, mSize))
            {
                var requiredSize = new Vec2
                {
                    X = position.X + 1,
                    Y = position.Y + 1
                };
                var newSize = new Vec2
                {
                    X = requiredSize.X > mSize.X ? requiredSize.X : mSize.X,
                    Y = requiredSize.Y > mSize.Y ? requiredSize.Y : mSize.Y
                };
                Resize(newSize);
            }
        }
        public Vec2 Size
        {
            get
            {
                return mSize;
            }
            set
            {
                if (value == mSize)
                {
                    return;
                }
                Resize(value);
            }
        }
        public char this[Vec2 position]
        {
            get
            {
                VerifyPosition(position);
                return mBuffer[Util.FlattenPosition(position, mSize.X)];
            }
            set
            {
                VerifyPosition(position);
                mBuffer[Util.FlattenPosition(position, mSize.X)] = value;
            }
        }
        private Vec2 mSize;
        private List<char> mBuffer;
    }
    internal sealed class Renderer : IRenderer
    {
        public Renderer(ConsoleFrontend frontend)
        {
            mFrontend = frontend;
            mBuffer = new RenderBuffer();
        }
        public void Present()
        {
            string text = "";
            for (int y = 0; y < mBuffer.Size.Y; y++)
            {
                for (int x = 0; x < mBuffer.Size.X; x++)
                {
                    text += mBuffer[new Vec2(x, y)];
                }
                if (y < mBuffer.Size.Y - 1)
                {
                    text += '\n';
                }
            }
            System.Console.SetCursorPosition(0, 0);
            System.Console.Write(text);
        }
        public void Render(Board board)
        {
            var border = GetBorder(new Vec2(board.Width, board.Height));
            foreach (Vec2 position in border)
            {
                mBuffer[position] = GetBorderCharacter(position, border);
            }
            RenderPieces(board);
        }
        private static Vec2 GetRenderPosition(Vec2 logicalPosition)
        {
            return new Vec2(1) + (logicalPosition * 2);
        }
        private static HashSet<Vec2> GetBorder(Vec2 size)
        {
            var border = new HashSet<Vec2>();
            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    var tilePos = GetRenderPosition(new Vec2(x, y));
                    for (int _x = tilePos.X - 1; _x < tilePos.X + 2; _x++)
                    {
                        for (int _y = tilePos.Y - 1; _y < tilePos.Y + 2; _y++)
                        {
                            var position = new Vec2(_x, _y);
                            if (position != tilePos)
                            {
                                border.Add(position);
                            }
                        }
                    }
                }
            }
            return border;
        }
        private static char GetBorderCharacter(Vec2 characterPosition, HashSet<Vec2> positions)
        {
            const int UP = 0b0001;
            const int DOWN = 0b0010;
            const int LEFT = 0b0100;
            const int RIGHT = 0b1000;
            int surroundings = 0;
            foreach (Vec2 position in positions)
            {
                var difference = position - characterPosition;
                if (difference.TaxicabLength() != 1)
                {
                    continue;
                }
                surroundings |= (difference.X, difference.Y) switch
                {
                    (0, -1) => UP,
                    (0, 1) => DOWN,
                    (-1, 0) => LEFT,
                    (1, 0) => RIGHT,
                    _ => 0
                };
            }
            return surroundings switch
            {
                UP | DOWN => '\u2551',
                LEFT | RIGHT => '\u2550',
                DOWN | RIGHT => '\u2554',
                DOWN | LEFT => '\u2557',
                UP | RIGHT => '\u255A',
                UP | LEFT => '\u255D',
                DOWN | LEFT | RIGHT => '\u2566',
                UP | LEFT | RIGHT => '\u2569',
                UP | DOWN | RIGHT => '\u2560',
                UP | DOWN | LEFT => '\u2563',
                UP | DOWN | LEFT | RIGHT => '\u256C',
                _ => ' '
            };
        }
        private static char GetPieceCharacter(Piece piece)
        {
            char pieceCharacter = piece.Type switch
            {
                PieceType.Pawn => 'P',
                PieceType.Knight => 'N',
                PieceType.Bishop => 'B',
                PieceType.Rook => 'R',
                PieceType.Queen => 'Q',
                PieceType.King => 'K',
                _ => throw new Exception("Invalid piece type!")
            };
            int colorOffset = (piece.Color == PieceColor.Black) ? 'a' - 'A' : 0;
            return (char)(pieceCharacter + colorOffset);
        }
        private void RenderPieces(Board board)
        {
            for (int x = 0; x < board.Width; x++)
            {
                for (int y = 0; y < board.Height; y++)
                {
                    var position = new Vec2(x, y);
                    Tile tile = board[position];
                    if (tile.Piece != null)
                    {
                        var renderPosition = GetRenderPosition(position);
                        char character = GetPieceCharacter(tile.Piece);
                        mBuffer[renderPosition] = character;
                    }
                }
            }
        }
        public void ClearBuffer()
        {
            mBuffer.Clear();
        }
        public IFrontend Frontend { get { return mFrontend; } }
        private readonly ConsoleFrontend mFrontend;
        private readonly RenderBuffer mBuffer;
    }
}