using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public sealed class Util
    {
        public static int FlattenPosition(Vec2 position, int width)
        {
            return (position.Y * width) + position.X;
        }
        public static bool IsOutOfRange(Vec2 position, Vec2 size)
        {
            bool x = position.X >= size.X;
            bool y = position.Y >= size.Y;
            return x || y;
        }
    }
}
