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
            Assert.Equal("Empty", opt.ToString());
            Assert.Equal(Optional.Empty<int>().GetHashCode(), opt.GetHashCode());
            Assert.Equal(Optional.Empty<int>(), opt);
            Assert.Equal(456, opt.OrElse(456));
            Assert.Equal(789, opt.OrElseGet(() => 789));
            Assert.Throws<InvalidOperationException>(() => opt.OrElseThrow<InvalidOperationException>());

            var receivedVal = 0;
            opt.IfHasValue(val => receivedVal = val);
            Assert.Equal(0, receivedVal);

            var stringOpt = opt.Map(val => val.ToString());
            Assert.False(stringOpt.HasValue);

            Assert.Equal(Optional.Empty<int>(), opt.Filter(val => (val == 123)));
        }

        [Fact]
        public void TestOptionalWithValue()
        {
            var opt = Optional.FromValue(123);
            Assert.True(opt.HasValue);
            Assert.Equal(123, opt.Value);
            Assert.Equal("123", opt.ToString());
            Assert.Equal(Optional.FromValue(123).GetHashCode(), opt.GetHashCode());
            Assert.Equal(Optional.FromValue(123), opt);
            Assert.Equal(123, opt.OrElse(456));
            Assert.Equal(123, opt.OrElseGet(() => 789));
            Assert.Equal(123, opt.OrElseThrow<InvalidOperationException>());
    
            var receivedVal = 0;
            opt.IfHasValue(val => receivedVal = val);
            Assert.Equal(123, receivedVal);

            var stringOpt = opt.Map(val => val.ToString());
            Assert.True(stringOpt.HasValue);
            Assert.Equal("123", stringOpt.Value);

            var nullMap = opt.Map<string>(val => null);
            Assert.False(nullMap.HasValue);
            Assert.Equal(Optional.Empty<string>().GetHashCode(), nullMap.GetHashCode());

            Assert.Equal(Optional.FromValue(123), opt.Filter(val => (val == 123)));
            Assert.Equal(Optional.Empty<int>(), opt.Filter(val => (val == 456)));
        }

        [Fact]
        public void TestOptionalWithoutValue()
        {
            string nullX = null;
            var opt = Optional.FromNullable(nullX);
            Assert.Throws<InvalidOperationException>(() => opt.Value);
            Assert.False(opt.HasValue);
            Assert.Equal("Empty", opt.ToString());
            Assert.Equal(Optional.Empty<int>().GetHashCode(), opt.GetHashCode());
            Assert.Equal(Optional.Empty<string>(), opt);
            Assert.Equal("abc", opt.OrElse("abc"));
            Assert.Equal("def", opt.OrElseGet(() => "def"));
            Assert.Throws<InvalidOperationException>(() => opt.OrElseThrow<InvalidOperationException>());

            string receivedVal = string.Empty;
            opt.IfHasValue(val => receivedVal = val);
            Assert.Equal(string.Empty, receivedVal);

            var stringOpt = opt.Map(val => val.ToString());
            Assert.False(stringOpt.HasValue);

            Assert.Equal(Optional.Empty<string>(), opt.Filter(val => val == "456"));
        }

        // Map
        // FlatMap

    }
}
