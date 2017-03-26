using System;
using Xunit;

namespace Optional.Tests
{
    public class Optional_UnitTest
    {
        [Fact]
        public void TestHasEmpty()
        {
            var x = Optional.From(123);
            Assert.True(x.HasValue);

            int? y = null;
            var x2 = Optional.FromNullable(y);
            Assert.False(x2.HasValue);

            Assert.False(Optional.Empty<int>().HasValue);
        }
    }
}
