namespace LidarObjectDetection.Utilities;



public class Optional {

	public static readonly Optional NoValue = new();

	private Optional() { }

}



//public static class OptionalExtensions


public class Optional<T> {

	private readonly T Value;
	private bool HasValue { get; }



	private Optional() {

		HasValue = false;
		Value = default!;
	}

	public Optional(T value) {

		HasValue = true;
		Value = value;
	}



	public void Match(Action<T> hasValueAction, Action noValueAction) {

		if (HasValue) {
			hasValueAction.Invoke(Value);
			return;
		}

		noValueAction.Invoke();
	}

	public TResult Match<TResult>(Func<T, TResult> hasValueAction, Func<TResult> noValueAction) {

		return HasValue
			? hasValueAction.Invoke(Value)
			: noValueAction.Invoke();
	}



	private bool Equals(Optional<T> other) {
		return EqualityComparer<T>.Default.Equals(Value, other.Value) && HasValue == other.HasValue;
	}

	public override bool Equals(object? obj) {

		if (ReferenceEquals(null, obj)) {
			return false;
		}

		if (ReferenceEquals(this, obj)) {
			return true;
		}

		return obj.GetType() == GetType() && Equals((Optional<T>)obj);
	}

	public override int GetHashCode() {
		return HashCode.Combine(Value, HasValue);
	}



	public static bool operator ==(Optional<T> left, T? right) {

		return left.HasValue && left.Value!.Equals(right);
	}

	public static bool operator !=(Optional<T> left, T? right) {

		return !(left == right);
	}

	public static bool operator ==(T? left, Optional<T> right) {

		return right == left;
	}

	public static bool operator !=(T? left, Optional<T> right) {

		return !(left == right);
	}

	public static bool operator ==(Optional<T> left, Optional<T> right) {

		if (!left.HasValue) {
			return !right.HasValue;
		}

		return right.HasValue && left.Value!.Equals(right.Value);
	}

	public static bool operator !=(Optional<T> left, Optional<T> right) {
		return !(left == right);
	}



	public static implicit operator Optional<T>(Optional noValue) {
		return new();
	}

	public static implicit operator Optional<T>(T value) {

		return new(value);
	}

}