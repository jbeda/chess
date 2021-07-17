using System;
using System.Runtime.InteropServices;

namespace Chess
{
    // Vec2 represents a position on the board using just numbers.  It aligns
    // with standard Algebraic notation in that a1 is (0,0). 
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
        // This is the column (file) on the board (a-h in algebraic notation).
        // It is zero based.
        public int X { get; set; }

        // This is the row (rank) on the board (1-8 in algebraic notation). It
        // is zero based.
        public int Y { get; set; }

        public static implicit operator Vec2((int x, int y) t) => new Vec2(t.x, t.y);
        public void Deconstruct(out int x, out int y) {
            x = X;
            y = Y;
        }

        public static Vec2 FromAlgebraic(string alg) {
            var v = new Vec2(0);
            v.Algebraic = alg;
            return v;
        }
        public string Algebraic {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
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
