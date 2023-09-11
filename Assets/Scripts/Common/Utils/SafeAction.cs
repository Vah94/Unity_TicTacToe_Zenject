using System;

namespace Common.Utils
{
    public interface ISafeAction<out T>
    {
        void Subscribe(Action<T> action);
        void Unsubscribe(Action<T> action);
        void SetOneTimeAction(Action<T> action);
    }

    public interface ISafeAction
    {
        void Subscribe(Action action);
        void Unsubscribe(Action action);

        void SetOneTimeAction(Action action);
    }

    public class SafeAction<T> : ISafeAction<T>
    {
        protected Action<T> _innerAction;
        protected Action<T> _callOnChangeAction;

        public SafeAction()
        {
            _innerAction = null;
        }

        public void Invoke(T data)
        {
            _innerAction?.Invoke(data);
            _callOnChangeAction?.Invoke(data);
            _callOnChangeAction = null;
        }

        public void Subscribe(Action<T> action)
        {
            _innerAction += action;
        }


        public void Unsubscribe(Action<T> action)
        {
            _innerAction -= action;
        }

        public void SetOneTimeAction(Action<T> action)
        {
            _callOnChangeAction += action;
        }
    }

    public class SafeAction : ISafeAction
    {
        protected Action _innerAction;
        protected Action _callOnChangeAction;


        public SafeAction()
        {
            _innerAction = null;
        }

        public void Invoke()
        {
            _innerAction?.Invoke();
            _callOnChangeAction?.Invoke();
            _callOnChangeAction = null;
        }

        public void Subscribe(Action action)
        {
            _innerAction += action;
        }


        public void Unsubscribe(Action action)
        {
            _innerAction -= action;
        }

        public void SetOneTimeAction(Action action)
        {
            _callOnChangeAction += action;
        }
    }
}