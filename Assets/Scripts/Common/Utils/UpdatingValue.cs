namespace Common.Utils
{
    public class UpdatingValue<T> : SafeAction, IUpdatingValue<T>
    {
        private T _value;
        public T PreviousValue { get; private set; }


        public T Value
        {
            get => _value;
            set
            {
                PreviousValue = _value;
                _value = value;
                _innerAction?.Invoke();
            }
        }

        public override string ToString()
        {
            return $"Value: {Value}\nPreviousValue: {PreviousValue}";
        }
    }

    public interface IUpdatingValue<out T> : ISafeAction
    {
        T PreviousValue { get; }
        T Value { get; }
    }
}