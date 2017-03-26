using System;
using Xunit;

namespace Optional.Tests
{
    public class Optional_UnitTest
    {
        [Fact]
        public void TestEmpty()
        {
            var opt = Optional.Empty<int>();
            Assert.False(opt.HasValue);
            Assert.Equal(opt.ToString(), "Empty");
        }

        [Fact]
        public void TestOptionalWithValue()
        {
            var opt = Optional.From(123);
            Assert.True(opt.HasValue);
            Assert.Equal(opt.ToString(), "123");
        }

        [Fact]
        public void TestOptionalWithoutValue()
        {
            int? nullX = null;
            var opt = Optional.FromNullable(nullX);
            Assert.False(opt.HasValue);
            Assert.Equal(opt.ToString(), "Empty");
        }


        // TODO
        // nullable types
        // equals and hascode
        // Value
        // ToString
        // OrElse
        // OrElseGet
        // OrElseThrow

    }
}
