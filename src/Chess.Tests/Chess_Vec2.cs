using System;
using Xunit;

namespace Chess.Tests
{
    public class Vec2_Algebraic {
        [Theory]
        [InlineData("a1", 0, 0)]
        [InlineData("c1", 2, 0)]
        [InlineData("a8", 0, 7)]
        [InlineData("h1", 7, 0)]
        [InlineData("h8", 7, 7)]
        public void Set(string algebraic, int expectedX, int expectedY) 
        {
            var v = new Vec2();
            v.Algebraic = algebraic;
            Assert.Equal(expectedX, v.X);
            Assert.Equal(expectedY, v.Y);
        }

        [Theory]
        [InlineData("a9")]
        [InlineData("")]
        [InlineData("a99")]
        [InlineData("z0")]
        [InlineData("foo")]
        public void SetException(string algebraic)
        {
            var v = new Vec2();
            Assert.Throws<ArgumentException>(() => v.Algebraic = algebraic);
        }


        [Theory]
        [InlineData(0, 0, "a1")]
        [InlineData(2, 0, "c1")]
        [InlineData(0, 7, "a8")]
        [InlineData(7, 0, "h1")]
        [InlineData(7, 7, "h8")]
        public void Get(int x, int y, string algebraicExpected)
        {
            var v = new Vec2(x, y);
            Assert.Equal(algebraicExpected, v.Algebraic);
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(-1, -1)]
        [InlineData(8, 0)]
        [InlineData(0, 8)]
        public void GetExcpetion(int x, int y) {
            var v = new Vec2(x, y);
            Assert.Throws<ArgumentException>(() => v.Algebraic);
        }
    }
}