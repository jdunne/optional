using System;
using System.Collections.Generic;

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

    public struct Optional<T> : IEquatable<Optional<T>>
    {
        private readonly T _value;
        private readonly bool _hasValue;

        internal Optional(T value, bool hasValue)
        {
            _value = value;
            _hasValue = hasValue;
        }

        public bool HasValue => _hasValue;

        public T Value 
        {
            get 
            {
                if(!_hasValue)
                {
                    throw new InvalidOperationException("Cannot access value when not present.");
                }
                return _value;
            }
        }

        public override string ToString()
        {
            return _hasValue ? _value.ToString() : "Empty";
        }



        public override bool Equals (object obj)
        {
            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((Optional<T>)obj);
        }
        

        public override int GetHashCode()
        {
            return _hasValue ? _value.GetHashCode() : 0;
        }

        public bool Equals(Optional<T> other)
        {
            if(!HasValue && !other.HasValue)
            {
                return true;
            }
            else if( HasValue && other.HasValue )
            {
                return EqualityComparer<T>.Default.Equals( _value, other._value );
            }
            return false;
        }
    }
}
