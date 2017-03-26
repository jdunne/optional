using System;

namespace Optional
{
    public class Optional
    {
        static public Optional<T> From<T>(T val)
        {
            if(val == null)
            {
                throw new NullReferenceException(nameof(val));
            }
            return new Optional<T>( val, true );
        }

        static public Optional<T> FromNullable<T>(T val)
        {
            var hasValue = (val != null);
            return new Optional<T>( val, hasValue );
        }

        static public Optional<T> Empty<T>()
        {
            return new Optional<T>( default(T), false );
        }
    }

    public struct Optional<T>
    {
        private readonly T _value;
        private readonly bool _hasValue;

        internal Optional(T value, bool hasValue)
        {
            _value = value;
            _hasValue = hasValue;
        }

        public bool HasValue => _hasValue;


    }
}
