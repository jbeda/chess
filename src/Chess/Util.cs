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
    }
}
