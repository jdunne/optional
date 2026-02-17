using System;
using System.Collections.Generic;

namespace Optional
{
    public class Optional
    {
        /// <summary>Creates an Optional from a reference type.</summary>
        /// <param name="val">Must not be null.</param>
        /// <exception cref="ArgumentNullException">Thrown ArgumentNullException if val is null.</exception>
        static public Optional<T> From<T>(T val) where T : class
        {
            ArgumentNullException.ThrowIfNull(val);
            return new Optional<T>(val, true);
        }

        /// <summary>Creates an Optional from a reference type.</summary>
        /// <param name="val">May be null.</param>
        static public Optional<T> FromNullable<T>(T val) where T : class
        {
            return (val == null) ? Empty<T>() : From<T>(val);
        }

        /// <summary>Creates an Optional from a value type.</summary>
        static public Optional<T> FromValue<T>(T val) where T : struct
        {
            return new Optional<T>(val, true);
        }

        /// <summary>Returns an empty optional.</summary>
        static public Optional<T> Empty<T>()
        {
            return new Optional<T>(default(T), false);
        }
    }

    /// <summary> An Optional can either hold a value or hold nothing. </summary>
    public struct Optional<T> : IEquatable<Optional<T>>
    {
        private readonly T _value;

        internal Optional(T value, bool hasValue)
        {
            _value = value;
            HasValue = hasValue;
        }

        /// <summary>Returns true if a value is present.</summary>
        public bool HasValue { get; private set; }

        /// <summary>Returns a value if it is present, else throws an exception</summary>
        /// <exception cref="InvalidOperationException">Thrown when HasValue is false.</exception>
        public T Value
        {
            get
            {
                if (!HasValue)
                {
                    throw new InvalidOperationException("Cannot access value when not present.");
                }
                return _value;
            }
        }

        public override readonly string ToString()
        {
            return HasValue ? _value.ToString() : "Empty";
        }

        public T OrElse(T otherValue)
        {
            return HasValue ? _value : otherValue;
        }

        public T OrElseGet(Func<T> factory)
        {
            ArgumentNullException.ThrowIfNull(factory);
            return HasValue ? _value : factory();
        }

        public T OrElseThrow<E>() where E : System.Exception, new()
        {
            if (!HasValue)
            {
                throw new E();
            }
            return _value;
        }

        public void IfHasValue(Action<T> action)
        {
            ArgumentNullException.ThrowIfNull(action);
            if (HasValue)
            {
                action(_value);
            }
        }

        public Optional<T> Filter(Predicate<T> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return !HasValue || predicate(_value) ?
                this :
                Optional.Empty<T>();
        }

        public Optional<U> Map<U>(Func<T, U> transform)
        {
            ArgumentNullException.ThrowIfNull(transform);
            if (!HasValue)
            {
                return Optional.Empty<U>();
            }
            var newValue = transform(_value);
            if (object.Equals(newValue, null))
            {
                return Optional.Empty<U>();
            }
            return new Optional<U>(newValue, true);
        }

        public override readonly bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((Optional<T>)obj);
        }

        public override readonly int GetHashCode()
        {
            return HasValue ? _value.GetHashCode() : 0;
        }

        public readonly bool Equals(Optional<T> other)
        {
            if (!HasValue && !other.HasValue)
            {
                return true;
            }
            else if (HasValue && other.HasValue)
            {
                return EqualityComparer<T>.Default.Equals(_value, other._value);
            }
            return false;
        }
    }
}
