using System;
using System.Collections.Generic;

namespace Optional
{
    public class Optional
    {
        /// <summary>Creates an <see cref="Optional{T}"/> from a non-null reference value.</summary>
        /// <typeparam name="T">The reference type to wrap.</typeparam>
        /// <param name="val">The value to wrap. Must not be null.</param>
        /// <returns>An <see cref="Optional{T}"/> containing <paramref name="val"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="val"/> is null.</exception>
        static public Optional<T> From<T>(T val) where T : class
        {
            ArgumentNullException.ThrowIfNull(val);
            return new Optional<T>(val, true);
        }

        /// <summary>Creates an <see cref="Optional{T}"/> from a nullable reference value.</summary>
        /// <typeparam name="T">The reference type to wrap.</typeparam>
        /// <param name="val">The value to wrap. May be null.</param>
        /// <returns>
        /// An empty <see cref="Optional{T}"/> when <paramref name="val"/> is null; otherwise an
        /// <see cref="Optional{T}"/> containing <paramref name="val"/>.
        /// </returns>
        static public Optional<T> FromNullable<T>(T val) where T : class
        {
            return (val == null) ? Empty<T>() : From<T>(val);
        }

        /// <summary>Creates an <see cref="Optional{T}"/> from a value type.</summary>
        /// <typeparam name="T">The value type to wrap.</typeparam>
        /// <param name="val">The value to wrap.</param>
        /// <returns>An <see cref="Optional{T}"/> containing <paramref name="val"/>.</returns>
        static public Optional<T> FromValue<T>(T val) where T : struct
        {
            return new Optional<T>(val, true);
        }

        /// <summary>Returns an empty <see cref="Optional{T}"/>.</summary>
        /// <typeparam name="T">The type parameter for the optional value.</typeparam>
        /// <returns>An empty <see cref="Optional{T}"/>.</returns>
        static public Optional<T> Empty<T>()
        {
            return new Optional<T>(default(T), false);
        }
    }

    /// <summary>Represents an optional value that may or may not be present.</summary>
    /// <typeparam name="T">The wrapped value type.</typeparam>
    public struct Optional<T> : IEquatable<Optional<T>>
    {
        private readonly T _value;

        internal Optional(T value, bool hasValue)
        {
            _value = value;
            HasValue = hasValue;
        }

        /// <summary>Gets a value indicating whether a value is present.</summary>
        public bool HasValue { get; private set; }

        /// <summary>Gets the wrapped value when it is present.</summary>
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

        /// <summary>Returns a string representation of this optional value.</summary>
        /// <returns>The wrapped value text when present; otherwise <c>Empty</c>.</returns>
        public override readonly string ToString()
        {
            return HasValue ? _value.ToString() : "Empty";
        }

        /// <summary>Returns the wrapped value when present; otherwise returns a fallback value.</summary>
        /// <param name="otherValue">The fallback value to return when this optional is empty.</param>
        /// <returns>The wrapped value if present; otherwise <paramref name="otherValue"/>.</returns>
        public T OrElse(T otherValue)
        {
            return HasValue ? _value : otherValue;
        }

        /// <summary>Returns the wrapped value when present; otherwise computes a fallback value.</summary>
        /// <param name="factory">A function that produces the fallback value when this optional is empty.</param>
        /// <returns>The wrapped value if present; otherwise the value produced by <paramref name="factory"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory"/> is null.</exception>
        public T OrElseGet(Func<T> factory)
        {
            ArgumentNullException.ThrowIfNull(factory);
            return HasValue ? _value : factory();
        }

        /// <summary>Returns the wrapped value when present; otherwise throws a new exception of type <typeparamref name="E"/>.</summary>
        /// <typeparam name="E">The exception type to throw when this optional is empty.</typeparam>
        /// <returns>The wrapped value if present.</returns>
        /// <exception cref="E">Thrown when no value is present.</exception>
        public T OrElseThrow<E>() where E : System.Exception, new()
        {
            if (!HasValue)
            {
                throw new E();
            }
            return _value;
        }

        /// <summary>Invokes an action with the wrapped value when it is present.</summary>
        /// <param name="action">The action to invoke when a value is present.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="action"/> is null.</exception>
        public void IfHasValue(Action<T> action)
        {
            ArgumentNullException.ThrowIfNull(action);
            if (HasValue)
            {
                action(_value);
            }
        }

        /// <summary>Returns this optional when empty or when the value matches a predicate; otherwise returns empty.</summary>
        /// <param name="predicate">The predicate used to test the wrapped value.</param>
        /// <returns>
        /// This optional when it is empty or <paramref name="predicate"/> returns <c>true</c>;
        /// otherwise an empty <see cref="Optional{T}"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="predicate"/> is null.</exception>
        public Optional<T> Filter(Predicate<T> predicate)
        {
            ArgumentNullException.ThrowIfNull(predicate);
            return !HasValue || predicate(_value) ?
                this :
                Optional.Empty<T>();
        }

        /// <summary>Transforms the wrapped value to another type when present.</summary>
        /// <typeparam name="U">The result value type.</typeparam>
        /// <param name="transform">The transformation to apply to the wrapped value.</param>
        /// <returns>
        /// An empty <see cref="Optional{U}"/> when this optional is empty; otherwise an
        /// <see cref="Optional{U}"/> containing the transformed value,
        /// or empty when the transform returns null.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="transform"/> is null.</exception>
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

        /// <summary>Determines whether this instance equals a specified object.</summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><c>true</c> if the object is an <see cref="Optional{T}"/> with equal state and value; otherwise <c>false</c>.</returns>
        public override readonly bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((Optional<T>)obj);
        }

        /// <summary>Returns the hash code for this optional value.</summary>
        /// <returns>The wrapped value hash code when present; otherwise 0.</returns>
        public override readonly int GetHashCode()
        {
            return HasValue ? _value.GetHashCode() : 0;
        }

        /// <summary>Determines whether this instance equals another optional value.</summary>
        /// <param name="other">The optional value to compare.</param>
        /// <returns><c>true</c> if both optionals are empty or both contain equal values; otherwise <c>false</c>.</returns>
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
