using System;
using System.Runtime.InteropServices;

namespace Chess
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vec2
    {
        public Vec2(int scalar)
        {
            X = Y = scalar;
        }
        public Vec2(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int TaxicabLength()
        {
            return Math.Abs(X) + Math.Abs(Y);
        }
        public static Vec2 operator+(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.X + v2.X, v1.Y + v2.Y);
        }
        public static Vec2 operator+(Vec2 vector, int scalar)
        {
            return new Vec2(vector.X + scalar, vector.Y + scalar);
        }
        public static Vec2 operator-(Vec2 vector)
        {
            return new Vec2(-vector.X, -vector.Y);
        }
        public static Vec2 operator-(Vec2 v1, Vec2 v2)
        {
            return v1 + -v2;
        }
        public static Vec2 operator-(Vec2 vector, int scalar)
        {
            return vector + -scalar;
        }
        public static Vec2 operator*(Vec2 vector, int scalar)
        {
            return new Vec2(vector.X * scalar, vector.Y * scalar);
        }
        public static Vec2 operator/(Vec2 vector, int scalar)
        {
            return new Vec2(vector.X / scalar, vector.Y / scalar);
        }
        public static bool operator==(Vec2 v1, Vec2 v2)
        {
            return (v1.X == v2.X) && (v1.Y == v2.Y);
        }
        public static bool operator!=(Vec2 v1, Vec2 v2)
        {
            return !(v1 == v2);
        }
        public override bool Equals(object obj)
        {
            if (obj is Vec2 vec)
            {
                return this == vec;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return Tuple.Create(X, Y).GetHashCode();
        }
    }
}
