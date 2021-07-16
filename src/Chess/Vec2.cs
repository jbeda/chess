using System.Runtime.InteropServices;

namespace Chess
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vec2
    {
        public Vec2(int scalar)
        {
            this.X = this.Y = scalar;
        }
        public Vec2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

    }
}
