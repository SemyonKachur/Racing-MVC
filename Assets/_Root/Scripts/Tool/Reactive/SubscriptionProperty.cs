using System;

namespace Tool
{
    public partial class SubscriptionProperty<TValue> : ISubscriptionProperty<TValue>
    {
        private TValue _value;
        private Action<TValue> _onChangeValue;

        public TValue Value
        {
            get => _value;
            set
            {
                _value = value;
                _onChangeValue?.Invoke(_value);
            }
        }
        
        public SubscriptionProperty() {}
        public SubscriptionProperty(TValue value) => _value = value;

        public void SubscribeOnChange(Action<TValue> subscriptionAction) =>
            _onChangeValue += subscriptionAction;

        public void UnSubscribeOnChange(Action<TValue> unsubscriptionAction) =>
            _onChangeValue -= unsubscriptionAction;
    }
}
