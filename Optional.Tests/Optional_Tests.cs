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
            Assert.Throws<InvalidOperationException>(() => opt.Value);
            Assert.Equal(opt.ToString(), "Empty");
            Assert.Equal(opt.GetHashCode(), Optional.Empty<int>().GetHashCode());
            Assert.Equal(opt, Optional.Empty<int>());
            Assert.Equal(opt.OrElse(456), 456);
            Assert.Equal(opt.OrElseGet( () => 789), 789);
            Assert.Throws<InvalidOperationException>(() => opt.OrElseThrow<InvalidOperationException>());
        }

        [Fact]
        public void TestOptionalWithValue()
        {
            var opt = Optional.From(123);
            Assert.True(opt.HasValue);
            Assert.Equal(opt.Value, 123);
            Assert.Equal(opt.ToString(), "123");
            Assert.Equal(opt.GetHashCode(), Optional.From(123).GetHashCode());
            Assert.Equal(opt, Optional.From(123));
            Assert.Equal(opt.OrElse(456), 123);
            Assert.Equal(opt.OrElseGet( () => 789), 123);
            Assert.Equal(opt.OrElseThrow<InvalidOperationException>(), 123);
        }

        [Fact]
        public void TestOptionalWithoutValue()
        {
            int? nullX = null;
            var opt = Optional.FromNullable(nullX);
            Assert.Throws<InvalidOperationException>(() => opt.Value);
            Assert.False(opt.HasValue);
            Assert.Equal(opt.ToString(), "Empty");
            Assert.Equal(opt.GetHashCode(), Optional.Empty<int>().GetHashCode());
            Assert.Equal(opt, Optional.Empty<int?>());
            Assert.Equal(opt.OrElse(456), 456);
            Assert.Equal(opt.OrElseGet( () => 789), 789);
            Assert.Throws<InvalidOperationException>(() => opt.OrElseThrow<InvalidOperationException>());
        }


        // TODO
    
        // OrElseGet
        // OrElseThrow

    }
}
