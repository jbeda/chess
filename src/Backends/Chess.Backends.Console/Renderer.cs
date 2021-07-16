using System.Collections.Generic;

namespace Chess.Backends.Console
{
    internal sealed class RenderBuffer
    {
        public RenderBuffer()
        {
            this.mSize = new Vec2(0);
            this.mBuffer = new List<char>();
        }
        public void Clear()
        {
            for (int x = 0; x < this.mSize.X; x++)
            {
                for (int y = 0; y < this.mSize.Y; y++)
                {
                    this.mBuffer[Util.FlattenPosition(new Vec2(x, y), this.mSize.X)] = ' ';
                }
            }
        }
        public void Resize(Vec2 newSize)
        {
            Vec2 originalSize = this.mSize;
            this.mSize = newSize;
            var originalData = new List<char>();
            originalData.AddRange(this.mBuffer);
            this.mBuffer = new List<char>(this.mSize.X * this.mSize.Y);
            int lesserWidth = (this.mSize.X > originalSize.X) ? originalSize.X : this.mSize.X;
            int lesserHeight = (this.mSize.Y > originalSize.Y) ? originalSize.Y : this.mSize.Y;
            for (int x = 0; x < this.mSize.X; x++)
            {
                for (int y = 0; y < this.mSize.Y; y++)
                {
                    int index = Util.FlattenPosition(new Vec2(x, y), this.mSize.X);
                    if (x < lesserWidth && y < lesserHeight)
                    {
                        this.mBuffer[index] = originalData[Util.FlattenPosition(new Vec2(x, y), originalSize.X)];
                    }
                    else
                    {
                        this.mBuffer[index] = ' ';
                    }
                }
            }
        }
        private void VerifyPosition(Vec2 position)
        {
            if (Util.IsOutOfRange(position, this.mSize))
            {
                var requiredSize = new Vec2
                {
                    X = position.X - 1,
                    Y = position.Y - 1
                };
                var newSize = new Vec2
                {
                    X = requiredSize.X > this.mSize.X ? requiredSize.X : this.mSize.X,
                    Y = requiredSize.Y > this.mSize.Y ? requiredSize.Y : this.mSize.Y
                };
                this.Resize(newSize);
            }
        }
        public Vec2 Size
        {
            get
            {
                return this.mSize;
            }
            set
            {
                this.Resize(value);
            }
        }
        public char this[Vec2 position]
        {
            get
            {
                this.VerifyPosition(position);
                return this.mBuffer[Util.FlattenPosition(position, this.mSize.X)];
            }
            set
            {
                this.VerifyPosition(position);
                this.mBuffer[Util.FlattenPosition(position, this.mSize.X)] = value;
            }
        }
        private Vec2 mSize;
        private List<char> mBuffer;
    }
    internal sealed class Renderer : IRenderer
    {
        public Renderer(ConsoleBackend backend)
        {
            this.mBackend = backend;
            this.mBuffer = new RenderBuffer();
        }
        public void Present()
        {
            string text = "";
            for (int y = 0; y < this.mBuffer.Size.Y; y++)
            {
                for (int x = 0; x < this.mBuffer.Size.X; x++)
                {
                    text += this.mBuffer[new Vec2(x, y)];
                }
                if (y < this.mBuffer.Size.Y - 1)
                {
                    text += '\n';
                }
            }
            System.Console.Clear();
            System.Console.Write(text);
        }
        public void Render(Board board)
        {
            // todo: render board
        }
        public void ClearBuffer()
        {
            mBuffer.Clear();
        }
        public IBackend Backend { get { return this.mBackend; } }
        private readonly ConsoleBackend mBackend;
        private readonly RenderBuffer mBuffer;
    }
}