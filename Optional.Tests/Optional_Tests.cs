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

            var receivedVal = 0;
            opt.IfHasValue( val => receivedVal = val);
            Assert.Equal(receivedVal, 0);

            var stringOpt = opt.Map( val => val.ToString() );
            Assert.False(stringOpt.HasValue);

            Assert.Equal( opt.Filter( val => (val == 123) ),  Optional.Empty<int>() );
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
    
            var receivedVal = 0;
            opt.IfHasValue( val => receivedVal = val);
            Assert.Equal(receivedVal, 123);

            var stringOpt = opt.Map( val => val.ToString() );
            Assert.True(stringOpt.HasValue);
            Assert.Equal(stringOpt.Value, "123");

            Assert.Equal( opt.Filter( val => (val == 123) ),  Optional.From(123) );
            Assert.Equal( opt.Filter( val => (val == 456) ),  Optional.Empty<int>() );            
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

            var receivedVal = 0;
            opt.IfHasValue( val => receivedVal = val.Value);
            Assert.Equal(receivedVal, 0);

            var stringOpt = opt.Map( val => val.ToString() );
            Assert.False(stringOpt.HasValue);

            Assert.Equal( opt.Filter( val => ( (val ?? 0) == 456) ),  Optional.Empty<int?>() );            
            
        }

        // Map
        // FlatMap
        // Filter

    }
}
