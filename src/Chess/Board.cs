namespace Chess
{
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
